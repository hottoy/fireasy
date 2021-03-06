﻿using System.ComponentModel.DataAnnotations;

namespace Fireasy.Data.Validation
{
    /// <summary>
    /// 对手机号的验证，配置文件对应的键为 Mobile。
    /// </summary>
    public sealed class MobileAttribute : ConfigurableRegularExpressionAttribute
    {
        /// <summary>
        /// 初始化 <see cref="MobileAttribute"/> 类的新实例。
        /// </summary>
        public MobileAttribute()
            : base("Mobile", "^13[0-9]{9}|15[012356789][0-9]{8}|18[0-9][0-9]{8}|14[57][0-9]{8}|17[0678][0-9]{8}$")
        {
            ErrorMessage = "{0} 字段不符合手机的格式";
        }
    }
}
