﻿
namespace TestASPTodo.Models.Entities
{
    public class Employee
    {
        public Guid id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public decimal Salary { get; set; }
      
    }
}