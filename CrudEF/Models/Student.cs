﻿using System;
using System.Collections.Generic;

namespace CrudEF.Models;

public partial class Student
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public int? Age { get; set; }

    public string? Number { get; set; }
}
