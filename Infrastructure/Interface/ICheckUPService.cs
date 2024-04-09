using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface ICheckUPService
    {
        Task<CheckUP> AddCheckUP(int Id);
        Task<CheckUP> CheckUPDetails(int Id);
        Task<IEnumerable<CheckUP>> CheckUPList();
        Task<Response> SaveCheckUPList(CheckUP checkUP);
        Task<Response> SaveMedicineSpecification(MedicineDetail medicineDetail);
        Task<Response> SaveLABSpecification(LabTestItem lab);
    }
}
