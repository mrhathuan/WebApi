﻿using Shop_Nhi.Common;
using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Shop_Nhi.Models.DAO
{
    public class PostDAO
    {
        private DBConnect db = null;

        public PostDAO()
        {
            db = new DBConnect();
        }

        public IEnumerable<Post> List()
        {        
            return db.Posts.OrderByDescending(x => x.createDate);
        }

        public List<Post> ListAllByTag(string tag)
        {
            var result = (from p in db.Posts
                          join c in db.ContentTags
                          on p.ID equals c.ContentID
                          where c.TagID == tag
                          select p
                              );
            return result.OrderByDescending(x => x.createDate).ToList();
            
        }

        public Tag GetTag(string id)
        {
            return db.Tags.Find(id);
        }

        public void Save(Post post)
        {
            var result = db.Posts.Find(post.ID);            
            if(result == null || post.ID == 0)
            {
                post.ID = -1;
                post.createDate= DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                post.metatTitle = StringHelper.RemoveSpecialChars(post.name.Trim()).Replace(" ", "-").ToLower();
                post.status = true;
                var postNew = db.Posts.Add(post);
                db.SaveChanges();
                //Xử lý tag
                if (!string.IsNullOrEmpty(post.tag))
                {
                    string[] tags = post.tag.Trim().Split(',');
                    foreach (var tag in tags)
                    {
                        var tagId = StringHelper.RemoveSpecialChars(tag.Trim()).Replace(" ", "-");
                        var existedTag = this.CheckTag(tagId);

                        //insert to to tag table
                        if (!existedTag)
                        {
                            this.InsertTag(tagId, tag);
                        }

                        //insert to content tag
                        this.InsertContentTag(postNew.ID, tagId);

                    }
                }
            }
            else
            {
                result.name = post.name;
                result.description = post.description;
                result.image = post.image;
                result.metatTitle = StringHelper.RemoveSpecialChars(post.name.Trim()).Replace(" ", "-").ToLower();
                result.metaKeywords = post.metaKeywords;
                result.metaDescription = post.metaDescription;
                result.modifiedByID = post.modifiedByID;
                result.modifiedByDate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                result.detail = post.detail;
                result.tag = post.tag;
                db.SaveChanges();
                if (!string.IsNullOrEmpty(post.tag))
                {
                    this.RemoveAllContentTag(post.ID);
                    string[] tags = post.tag.Trim().Split(',');
                    foreach (var tag in tags)
                    {
                        var tagId = StringHelper.RemoveSpecialChars(tag.Trim()).Replace(" ", "-");
                        var existedTag = this.CheckTag(tagId);
                        //insert to to tag table
                        if (!existedTag)
                        {
                            this.InsertTag(tagId, tag);
                        }
                        //insert to content tag
                        this.InsertContentTag(post.ID, tagId);

                    }
                }
            }            
            
        }

        public IEnumerable<Tag> ListAllTag()
        {
            return db.Tags.ToList();
        }
       
        public void InsertTag(string id, string name)
        {
            var tag = new Tag();
            tag.ID = id;
            tag.Name = name;
            db.Tags.Add(tag);
            db.SaveChanges();
        }

        public void InsertContentTag(long postId, string tagId)
        {
            var contentTag = new ContentTag();
            contentTag.ContentID = postId;
            contentTag.TagID = tagId;
            db.ContentTags.Add(contentTag);
            db.SaveChanges();
        }
        public bool CheckTag(string id)
        {
            return db.Tags.Count(x => x.ID == id) > 0;
        }

        public void RemoveAllContentTag(long postId)
        {
            var result = db.ContentTags.Where(x => x.ContentID == postId).ToList();
            foreach (var item in result)
            {
                db.ContentTags.Remove(item);
                db.SaveChanges();
            }           
        }

        public List<Tag> ListTag(long postId)
        {
            var model = (from a in db.Tags
                         join b in db.ContentTags
                         on a.ID equals b.TagID
                         where b.ContentID == postId
                         select new
                         {
                             ID = b.TagID,
                             Name = a.Name
                         }).AsEnumerable().Select(x => new Tag()
                         {
                             ID = x.ID,
                             Name = x.Name
                         });
            return model.ToList();
        }

        public Post GetByID(long id)
        {
            return db.Posts.Find(id);
        }

        //Delete
        public bool Delete(long id)
        {
            try
            {
                var result = db.Posts.Find(id);
                db.Posts.Remove(result);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Change Status
        public bool ChangeStatus(long id)
        {
            var result = db.Posts.Find(id);
            result.status = !result.status;
            db.SaveChanges();
            return result.status;
        }
    }
}
