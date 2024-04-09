using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model
{
    public class Medicines
    {
        public int Id { get; set; }
        public int MedicineCategoryId { get; set; }
        public string? Code { get; set; }
        public string? MedicineName { get; set; }
        public int ManufactureId { get; set; }
        public int UnitId { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal SellPrice { get; set; }
        public int Quantity { get; set; }
        public int AddQuantity { get; set; }
        public string? StockKeepingUnit { get; set; }
        public string? Description { get; set; }
        public string? ExpiryDate { get; set; }
        public string? CreatedDate { get; set; }
        public string? ModifyBy { get; set; }
        public string? ModifyDate { get; set; }
    }
    public class MedicineCatVM: Medicines
    {
        public IEnumerable<MedicineCategory> medicineCategories { get; set; }
    }
}
