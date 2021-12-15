﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Senparc.CO2NET.Exceptions;
using Senparc.CO2NET.Trace;
using Senparc.Ncf.Core.AssembleScan;
using Senparc.Ncf.XncfBase;
using System;
using System.Linq;

namespace Senparc.Ncf.Core.Areas
{
    /// <summary>
    /// 对所有扩展 Area 进行注册
    /// </summary>
    public static class AreaRegister
    {
        /// <summary>
        /// 自动注册所有 Area
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="env"></param>
        /// <param name="eachRegsiterAction">遍历到每一个 Register 额外的操作</param>
        /// <returns></returns>
        public static IMvcBuilder AddNcfAreas(this IMvcBuilder builder, /*Microsoft.Extensions.Hosting.IHostEnvironment*/IWebHostEnvironment env, Action<IAreaRegister> eachRegsiterAction = null)
        {
            AssembleScanHelper.AddAssembleScanItem(assembly =>
            {
                try
                {
                    var areaRegisterTypes = assembly.GetTypes()
                                .Where(z => z.GetInterface(nameof(IAreaRegister)) != null)
                                .ToArray();
                    foreach (var registerType in areaRegisterTypes)
                    {
                        Console.WriteLine("areaRegisterTypes:" + registerType.FullName);
                        var register = Activator.CreateInstance(registerType, true) as IAreaRegister;
                        if (register != null)
                        {
                            Console.WriteLine("areaRegisterTypes run AuthorizeConfig:" + register.AareaPageMenuItems.FirstOrDefault()?.Url);

                            register.AuthorizeConfig(builder, env);//进行注册

                            Console.WriteLine("areaRegisterTypes run AuthorizeConfig finished:" + register.AareaPageMenuItems.FirstOrDefault()?.Url);

                            eachRegsiterAction?.Invoke(register);//执行额外的操作
                        }
                        else
                        {
                            SenparcTrace.BaseExceptionLog(new BaseException($"{registerType.Name} 类型没有实现接口 IAreaRegister！"));
                        }
                    }
                }
                catch (Exception ex)
                {
                    var title = "AddNcfAreas() 自动扫描程序集报告（非程序异常）：" + assembly.FullName;
                    var message = ex.ToString();
                    Console.WriteLine(title);
                    Console.WriteLine(message);
                    SenparcTrace.SendCustomLog(title, message);
                }
            }, false);
            return builder;
        }

        /// <summary>
        /// 启动带 Web 功能的 NCF 引擎（如不需要使用 Web，如 RazorPage，可以直接使用 <see cref="Senparc.Ncf.XncfBase.Register.StartEngine(IServiceCollection, IConfiguration)"/>）
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        /// <param name="addRazorPagesConfig">services.AddRazorPages() 的内部委托</param>
        /// <param name="eachRegsiterAction">遍历到每一个 Register 额外的操作</param>
        /// <returns></returns>
        public static string StartWebEngine(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env,
            Action<RazorPagesOptions>? addRazorPagesConfig = null,
            Action<IAreaRegister> eachRegsiterAction = null)
        {
            var builder = services.AddRazorPages(addRazorPagesConfig)
            //注册所有 Ncf 的 Area 模块（必须）
            .AddNcfAreas(env, eachRegsiterAction);

            return services.StartEngine(configuration, env);
        }
    }
}