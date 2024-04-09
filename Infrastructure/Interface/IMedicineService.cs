using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface IMedicineService
    {
        Task<MedicineCatVM> AddMedicines(int Id);
        Task<IEnumerable<Medicines>> GetMedicine();
        Task<Response> SaveMedicine(Medicines medicines);
        Task<Response> DeleteMedicine(int Id);
        Task<Medicines> AddQuantityMedicine(int Id);
        Task<Response> SaveQuantity(Medicines medicines);
        Task<MedicineCategory> AddMedicinesCAT(int Id);
        Task<IEnumerable<MedicineCategory>> GetMedicineCat();
        Task<Response> SaveMedicineCAT(MedicineCategory medicinescat);
        Task<Response> DeleteMedicineCAT(int Id);
    }
}
