﻿
using System.ComponentModel.DataAnnotations.Schema;

namespace Lu.Repository
{
    public interface IObjectState
    {
        [NotMapped]
        ObjectState ObjectState { get; set; }
    }
}