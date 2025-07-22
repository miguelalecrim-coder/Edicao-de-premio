﻿using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel
{
    [Table("UserIds")]
    public class UserDataModel : IUserVisitor
    {

        public Guid Id { get; set; }

        public UserDataModel()
        {
        }
    }
}