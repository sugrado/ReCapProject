﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarsinfoContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            using (CarsinfoContext context = new CarsinfoContext())
            {

                var result = from c in filter == null ? context.Cars : context.Cars.Where(filter)
                             join b in context.Brands on c.BrandId equals b.BrandId
                             join co in context.Colors on c.ColorId equals co.ColorId
                             select new CarDetailDto
                             {
                                 Id = c.Id,
                                 BrandId = b.BrandId,
                                 ColorId = co.ColorId,
                                 BrandName = b.BrandName,
                                 ColorName = co.ColorName,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 ModelYear = c.ModelYear,
                                 MinFindexPoint = c.MinFindexPoint,
                                 CoverPhoto = context.CarImages.Where(k => k.CarId == c.Id).FirstOrDefault().ImagePath == null ? @"\Images\default.png" : context.CarImages.Where(k => k.CarId == c.Id).FirstOrDefault().ImagePath
                             };
                return result.ToList();
            }
        }

        public CarDetailDto GetCarDetail(int carId)
        {
            using (CarsinfoContext context = new CarsinfoContext())
            {

                var result = from c in context.Cars.Where(c=>c.Id == carId)
                             join b in context.Brands on c.BrandId equals b.BrandId
                             join co in context.Colors on c.ColorId equals co.ColorId
                             select new CarDetailDto
                             {
                                 Id = c.Id,
                                 BrandId = b.BrandId,
                                 ColorId = co.ColorId,
                                 BrandName = b.BrandName,
                                 ColorName = co.ColorName,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 ModelYear = c.ModelYear,
                                 MinFindexPoint = c.MinFindexPoint
                             };
                return result.SingleOrDefault();
            }
        }
    }
}
