using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shark_Tech.BL.DTOs;

public class GeneralResult
{
    public bool Success { get; set; }
    public ResultError[] Errors { get; set; } = [];
}

public class GeneralResult<T> : GeneralResult
{
    public T? Data { get; set; }
}

public class GeneralResultCart<T> : GeneralResult
{
    public Guid CartId { get; set; }
    public T? Data { get; set; }
    public int TotalCount { get; set; } = 0;
    public decimal TotalPrice { get; set; } = 0;
}

public class ResultError
{
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
