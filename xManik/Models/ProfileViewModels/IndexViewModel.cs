using System;
using System.ComponentModel.DataAnnotations;

namespace xManik.Models.ProfileViewModels
{

    public class IndexViewModel
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ProfileImagePath { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; }
        public DateTime DateRegistered { get; set; }

        public string StatusMessage { get; set; }
    }
}