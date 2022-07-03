﻿using System.ComponentModel.DataAnnotations;

namespace ParkyAPI.Models.Dtos
{
    public class NationalParkDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public DateTime Create { get; set; }
        public DateTime Estaplished { get; set; }
    }
}
