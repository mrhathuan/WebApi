using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Nhi.Models.DAO
{
    public class SlideDAO
    {
        private DBConnect db = null;

        public SlideDAO()
        {
            db = new DBConnect();
        }

        public Slide GetSlide()
        {
            return db.Slides.SingleOrDefault(x => x.status == true);
        }

        public Slide GetById(int id)
        {
            return db.Slides.Find(id);
        }
        //list
        public List<Slide> GetListSlide()
        {
            return db.Slides.OrderBy(x => x.dislayOrder).ToList();
        }

        //Status = true
        public List<Slide> ListShowHome()
        {
            return db.Slides.Where(x=>x.status == true).OrderBy(x => x.dislayOrder).ToList();
        }

        public void Save(Slide slide)
        {
            var result = db.Slides.Find(slide.ID);
            if(result == null || slide.ID == 0)
            {
                slide.ID = -1;
                slide.status = true;
                db.Slides.Add(slide);
            }
            else
            {
                result.name = slide.name;
                result.image = slide.image;
                result.dislayOrder = slide.dislayOrder;
            }
            db.SaveChanges();
        }
  
        //xóa
        public bool Delete(int id)
        {
            try
            {
                var result = db.Slides.Find(id);
                db.Slides.Remove(result);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //Sửa trạng thái
        public bool ChangeStatus(int id)
        {
            var result = db.Slides.Find(id);
            result.status = !result.status;
            db.SaveChanges();
            return result.status;
        }   
    }
}