using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace xManik.Models
{
    public class MyDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)// Return a boolean value: true == IsValid, false != IsValid
        {
            DateTime d = Convert.ToDateTime(value);
            return d >= DateTime.Now; //Dates Greater than or equal to today are valid (true)

        }
    }

    public class RegisterOrderViewModel
    {
        public string UserId { get; set; }
        public string ServiceId { get; set; }

        [Required(ErrorMessage = "Необіхдно ввести час надання послуги")]
        [Display(Name = "Введіть час:")]
        [MyDate(ErrorMessage = "Невірна дата")]
        public DateTime ServiceTime { get; set; }
        
        [Display(Name = "Деталі замовлення:")]
        public string OrderDetails { get; set; }
    }
}
