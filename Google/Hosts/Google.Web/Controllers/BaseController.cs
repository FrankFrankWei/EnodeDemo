using ECommon.Extensions;
using ECommon.IO;
using ENode.Commanding;
using Google.Infrastructure.Extenstions;
using Google.Web.ActionFilters;
using Google.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Google.Web.Controllers
{
    public class BaseController : Controller
    {
        protected ICommandService _commandService;
        public BaseController(ICommandService commandService)
        {
            _commandService = commandService;
        }

        /// <summary>
        /// 异步执行命令
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="commandReturnType">命令返回类型（默认只要命令被handle就返回,也可等Event被Handle后返回）</param>
        /// <param name="millisecondsDelay">操作超时时间（毫秒)默认为15秒</param>
        /// <returns></returns>
        private Task<AsyncTaskResult<CommandResult>> ExecuteCommandAsync(ICommand command, CommandReturnType commandReturnType = CommandReturnType.CommandExecuted, int millisecondsDelay = 15000)
        {
            return _commandService.ExecuteAsync(command, commandReturnType).TimeoutAfter(millisecondsDelay);
        }

        /// <summary>
        /// 异步执行命令，并返回操作结果
        /// </summary>
        /// <param name="command"></param>
        /// <param name="commandReturnType">命令返回类型（默认只要命令被handle就返回,也可等Event被Handle后返回）</param>
        /// <param name="millisecondsDelay">操作超时时间（毫秒)默认为15秒</param>
        /// <param name="msg">操作成功时，返回的信息</param>
        /// <returns>OperationJsonResult</returns>
        protected async Task<ActionResult> ExecuteCommandAndReturnOperationResult(ICommand command, CommandReturnType commandReturnType = CommandReturnType.CommandExecuted, int millisecondsDelay = 15000, string msg = "操作成功")
        {
            try
            {
                var result = await ExecuteCommandAsync(command, commandReturnType, millisecondsDelay);
                if (!result.IsSuccess())
                {
                    return Json(new CustomJsonResult { Result = false, ErrorCode = "FAIL", Msg = result.GetErrorMessage() }, JsonRequestBehavior.AllowGet);
                }
                return Json(new CustomJsonResult { Result = true, Msg = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (TimeoutException)
            {
                return Json(new CustomJsonResult { Result = false, ErrorCode = "TIMEOUT", Msg = "操作超时" }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 不关心命令执行结果直接发送命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="msg">命令发送成功返回的消息</param>
        /// <returns>是否发送成功</returns>
        protected async Task<bool> SendCommandAsync(ICommand command)
        {
            var commandResult = await _commandService.SendAsync(command);
            if (commandResult.Status == AsyncTaskStatus.Failed || commandResult.Status == AsyncTaskStatus.IOException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 根据条件等待命令结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="getResult"></param>
        /// <param name="isResultValid"></param>
        /// <returns></returns>
        protected Task<T> WaitCommandResult<T>(Func<T> getResult, Func<T, bool> isResultValid)
        {
            TimeSpan waitTimeout = TimeSpan.FromSeconds(6);
            TimeSpan pollInterval = TimeSpan.FromMilliseconds(300);
            return TimerTaskFactory.StartNew<T>(getResult, isResultValid, pollInterval, waitTimeout);
        }

    }

}