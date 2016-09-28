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

        //thêm
        public void Create(Slide slide)
        {
            db.Slides.Add(slide);
            db.SaveChanges();
        }
        public Slide GetSlideById(int id)
        {
            return db.Slides.Find(id);
        }
        //sửa
        public bool Edit(Slide slide)
        {
            try
            {
                var result = db.Slides.Find(slide.ID);
                result.name = slide.name;
                result.image = slide.image;
                result.dislayOrder = slide.dislayOrder;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        //xóa
        public void Delete(int id)
        {
            var result = db.Slides.Find(id);
            db.Slides.Remove(result);
            db.SaveChanges();
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