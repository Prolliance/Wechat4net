using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wechat4net.QY;
using Wechat4net.QY.Define;

namespace Wechat4net.Demo
{
    public class WebApp
    {
        public void getUserInfo(string code, string appId)
        {
            /*
              * 此接口通常用于从微信企业号跳转至WebApp后获取当前用户身份。
              * code：通过微信重定向跳转后获取到的身份标识
              * appId：应用id
              * 
              * code参数获取方法可参考下面链接：
              * http://qydev.weixin.qq.com/wiki/index.php?title=%E4%BC%81%E4%B8%9A%E8%8E%B7%E5%8F%96code
              */

            try
            {
                WechatUser user = UserProvider.GetUserInfo(code);
            }
            catch (Exception ex)//ex.Message 异常信息
            {
                //todo...
                //...
            }
        }
    }
}