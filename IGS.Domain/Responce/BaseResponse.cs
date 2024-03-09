﻿using IGS.Domain.Enums;

namespace IGS.Domain.Responce
{
    public class BaseResponse<T>
    {
        public string Description { get; set; }

        public StatusCode StatusCode { get; set; }

        public T Data { get; set; }
    }
}
