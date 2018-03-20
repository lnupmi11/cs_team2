﻿namespace xManik.Models
{
    public class Artwork
    {
        public string Id { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public virtual Provider Provider { get; set; }
    }
}
