using Data;
using Infrastructure;
using Infrastructure.Enum;
using Infrastructure.Interface;
using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class MedicineService : IMedicineService
    {
        private readonly IDapper _dapper;
        public MedicineService(IDapper dapper)
        {
            _dapper = dapper;
        }
        public async Task<MedicineCatVM> AddMedicines(int Id)
        {
            var res = new MedicineCatVM();
            string sp = "select * from Medicines where Id=@Id";
            try
            {
                res = await _dapper.GetAsync<MedicineCatVM>(sp, new { Id }, commandType: CommandType.Text);
                if (res == null)
                {
                    res = new MedicineCatVM
                    {
                        Code = GenerateMedicineCode(),
                    };
                }
            }
            catch (Exception ex)
            {

            }
            res.medicineCategories = await GetMedicineCat();

            return res;
        }
        public async Task<IEnumerable<Medicines>> GetMedicine()
        {
            List<Medicines> medicines = new List<Medicines>();
            string sp = "select * from Medicines";
            try
            {
                medicines = await _dapper.GetAllAsync<Medicines>(sp, new { }, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {

            }
            return medicines;
        }
        public async Task<Response> SaveMedicine(Medicines medicines)
        {
            var res = new Response
            {
                StatusCode = ResponseStatus.Failed,
                Msg = "Failed"
            };
            string sp = "proc_SaveMedicine";
            try
            {
                res = await _dapper.GetAsync<Response>(sp, new
                {
                    medicines.Id,
                    medicines.MedicineCategoryId,
                    medicines.MedicineName,
                    medicines.ManufactureId,
                    medicines.Quantity,
                    medicines.Code,
                    medicines.Description,
                    medicines.UnitPrice,
                    medicines.ExpiryDate,
                    medicines.UnitId,
                    medicines.StockKeepingUnit,
                    medicines.SellPrice,
                }, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        public async Task<Response> DeleteMedicine(int Id)
        {
            var res = new Response
            {
                StatusCode = ResponseStatus.Failed,
                Msg = "Failed"
            };
            string sp = "delete from medicines where Id=@Id select 1 as statusCode,'Deleted Successfully !!' as Msg";
            try
            {
                res = await _dapper.GetAsync<Response>(sp, new { Id }, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        public async Task<Medicines> AddQuantityMedicine(int Id)
        {
            var res = new Medicines();
            string sp = "select * from Medicines where Id=@Id";
            try
            {
                res = await _dapper.GetAsync<Medicines>(sp, new { Id }, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        public async Task<Response> SaveQuantity(Medicines medicines)
        {
            var res = new Response
            {
                StatusCode = ResponseStatus.Failed,
                Msg = "Failed"
            };
            string sp = "UPDATE Medicines SET Quantity = Quantity + @AddQuantity WHERE Id = @Id SELECT 1 AS StatusCode, 'Quantity Updated Successfully !!' AS Msg";
            try
            {
                res = await _dapper.GetAsync<Response>(sp, new
                {
                    medicines.Id,
                    medicines.Quantity,
                    medicines.AddQuantity,
                }, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        public static string GenerateMedicineCode()
        {
            string format = "M{0:yyyyMMdd}123{1}";
            DateTime currentDate = DateTime.Now;
            Random random = new Random();
            /* string randomDatePart = currentDate.AddDays(random.Next(-365, 365)).ToString("yyyyMMdd")*/
            ;
            string randomDatePart = currentDate.ToString("yyyyMMdd");
            string randomNumberPart = new string("0123456789".Select(c => (char)('0' + random.Next(10))).ToArray());
            string generatedCode = string.Format(format, randomDatePart, randomNumberPart);
            return generatedCode;
        }
        public async Task<MedicineCategory> AddMedicinesCAT(int Id)
        {
            var res = new MedicineCategory();
            string sp = "select * from MedicineCategory where Id=@Id";
            try
            {
                res = await _dapper.GetAsync<MedicineCategory>(sp, new { Id }, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        public async Task<IEnumerable<MedicineCategory>> GetMedicineCat()
        {
            List<MedicineCategory> medicines = new List<MedicineCategory>();
            string sp = "select * from MedicineCategory";
            try
            {
                medicines = await _dapper.GetAllAsync<MedicineCategory>(sp, new { }, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {

            }
            return medicines;
        }
        public async Task<Response> SaveMedicineCAT(MedicineCategory medicines)
        {
            var res = new Response
            {
                StatusCode = ResponseStatus.Failed,
                Msg = "Failed"
            };
            string sp = "proc_SaveMCAT";
            try
            {
                res = await _dapper.GetAsync<Response>(sp, new
                {
                    medicines.Id,
                    medicines.Name,
                    medicines.Description,
                    medicines.CreatedDate,
                }, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        public async Task<Response> DeleteMedicineCAT(int Id)
        {
            var res = new Response
            {
                StatusCode = ResponseStatus.Failed,
                Msg = "Failed"
            };
            string sp = "delete from MedicineCategory where Id=@Id select 1 as statusCode,'Deleted Successfully !!' as Msg";
            try
            {
                res = await _dapper.GetAsync<Response>(sp, new { Id }, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {

            }
            return res;
        }
    }
}
