using LineCat.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LineCat.Web.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 全局默认分页数
        /// </summary>
        public int GlobalPageSize = 20;

        #region 基类做的异常处理
        protected override void OnException(ExceptionContext filterContext)
        {
            //此处进行异常记录，可以记录到数据库或文本，也可以使用其他日志记录组件。
            //通过filterContext.Exception来获取这个异常。
            Exception ex = filterContext.Exception;
            TxtLog.SaveErrorLog("控制器捕获异常", ex.ToString(), "基类异常处理程序");

            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                //ajax请求的action出现的异常处理
                filterContext.Result = this.Json(new { success = false, msg = ex.Message, total = 0, rows = "" });//后面2个属性是为了兼容easyui-grid
            }
            else
            {
                //重定向到异常显示页或执行其他异常处理方法
                string viewName = "Exception";
                ViewBag.Msg = ex.Message;
                filterContext.Result = this.View(viewName, ex);
            }

            //设置异常已经处理，不再抛给上层，比如applicationerror
            filterContext.ExceptionHandled = true;
        }
        #endregion

        public ActionResult Exception(object error)
        {
            Exception ex = error as Exception;
            HttpException httpEx = ex as HttpException;

            ViewBag.Msg = ex.Message;
            if (httpEx != null)
            {
                ViewBag.Msg = httpEx.ErrorCode.ToString();
            }

            return View();
        }
    }
}