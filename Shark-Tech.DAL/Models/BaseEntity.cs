﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shark_Tech.DAL;

public class BaseEntity<T>
{
    public T Id{ get; set; }
}
