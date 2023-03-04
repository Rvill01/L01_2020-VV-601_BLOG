﻿using System.ComponentModel.DataAnnotations;
namespace L01_2020_VV_601.Models
{
    public class publicaciones
    {
        [Key]
        public int publicacionId { get; set; }
        public string titulo { get; set; }

        public string descripcion { get; set; }

        public int? usuario { get; set; }

    }
}