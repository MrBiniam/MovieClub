﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MovieClub.Models
{
    public class OrderViewModel
    {
        public List<OrderItemViewModel> RentedMovies { get; set; }
    }

    public class OrderItemViewModel
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}
