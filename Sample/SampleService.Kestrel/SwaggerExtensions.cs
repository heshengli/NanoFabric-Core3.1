using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleService.Kestrel
{
    /// <summary>
    /// 添加控制器模块说明
    /// </summary>
    public class ApplyTagDescriptions : IDocumentFilter
    {
        /// <summary>
        /// Apply
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = new List<OpenApiTag>
            {
                new OpenApiTag { Name = "Status", Description = "健康检查" },
                new OpenApiTag { Name = "Values", Description = "Values测试服务接口" },
            };
        }
    }
    /// <summary>
    /// 操作过过滤器 添加通用参数等
    /// </summary>
    public class AssignOperationVendorExtensions : IOperationFilter
    {
        /// <summary>
        /// apply
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var fileAttr = context.ApiDescription.CustomAttributes().OfType<SwaggerFileUploadAttribute>().FirstOrDefault();
            if (fileAttr != null)
            {
                //operation.Parameters.Add("multipart/form-data");
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "file",
                    //In = "formData",
                    In = ParameterLocation.Query,
                    Description = "上传图片",
                    Required = (fileAttr as SwaggerFileUploadAttribute).Required,
                    //Type = "file"
                    Reference=new OpenApiReference() 
                    {
                        Type= ReferenceType.RequestBody
                    }
                });

            }
        }
    }
    /// <summary>
    /// 文件上传
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class SwaggerFileUploadAttribute : Attribute
    {
        /// <summary>
        /// Required
        /// </summary>
        public bool Required { get; private set; }
        /// <summary>
        /// SwaggerFileUploadAttribute
        /// </summary>
        /// <param name="Required"></param>
        public SwaggerFileUploadAttribute(bool Required = true)
        {
            this.Required = Required;
        }
    }
}
