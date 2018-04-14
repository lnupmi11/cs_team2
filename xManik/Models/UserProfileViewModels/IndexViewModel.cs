using System;
using System.ComponentModel.DataAnnotations;

namespace xManik.Models.UserProfileViewModels
{
    public class IndexViewModel
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string UserProfileImagePath { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; }
        public DateTime DateRegistered { get; set; }

        public string StatusMessage { get; set; }
    }
}