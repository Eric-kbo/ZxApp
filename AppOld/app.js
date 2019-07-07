//app.js
App({
  onLaunch: function () {
    var that = this;

    // 展示本地存储能力
    var logs = wx.getStorageSync('logs') || []
    logs.unshift(Date.now())
    wx.setStorageSync('logs', logs);
    //地址信息
    wx.getLocation({
      type: 'gcj02', //返回可以用于wx.openLocation的经纬度
      success: function (res) {
        var latitude = res.latitude
        var longitude = res.longitude
        var a = "" + latitude + "," + longitude + "";
        wx.request({
          url: that.getUrl("GetAreaLocation"),
          method: "POST",
          data: {
            location: a
          },
          success: function (rnt) {
            var result = JSON.parse(rnt.data);
            that.globalData.AreaInfoData = result.ResultInfo;
          }
        })
      }
    });

    //广告获取
    wx.request({
      url: that.getUrl("SelADEntity"),
      data: { Position: 1 },
      method: "POST",
      success: function (rnt) {
        var result = JSON.parse(rnt.data);
        that.globalData.ADInfoData = result.ResultInfo
      }
    });


    // 登录
    wx.login({
      success: res => {
        // 发送 res.code 到后台换取 openId, sessionKey, unionId
      }
    })
    // 获取用户信息
    wx.getSetting({
      success: res => {
        if (res.authSetting['scope.userInfo']) {
          // 已经授权，可以直接调用 getUserInfo 获取头像昵称，不会弹框
          wx.getUserInfo({
            success: res => {
              // 可以将 res 发送给后台解码出 unionId
              this.globalData.userInfo = res.userInfo

              // 由于 getUserInfo 是网络请求，可能会在 Page.onLoad 之后才返回
              // 所以此处加入 callback 以防止这种情况
              if (this.userInfoReadyCallback) {
                this.userInfoReadyCallback(res)
              }
            }
          })
        }
      }
    })
  },
  globalData: {
    userInfo: null
    ,AreaInfoData:null,
    ADInfoData:null
  },
  showerror(route){
    wx.showModal({
      title: '提示',
      content: route,
      showCancel: false,
      success: function (res) {
        if (res.confirm) {
          //wx.navigateBack({ delta: 1 })
        }
      }
    })
  },//错误弹窗
  IsInt(obj){
   var result=parseInt(obj);
   if (isNaN(result))
   {
     return false;
   }
    return true;
  },
  IsPhone(obj)
  {
    var phoneReg = /(^1[3|4|5|7|8]\d{9}$)|(^09\d{8}$)/; 
    if (!phoneReg.test(obj)) {
      return false;
    } 
    return true; 
  },//验证手机号
  IsEmail(obj)
  {
    var reg = new RegExp("^[a-z0-9]+([._\\-]*[a-z0-9])*@([a-z0-9]+[-a-z0-9]*[a-z0-9]+.){1,63}[a-z0-9]+$"); //正则
    if (!reg.test(obj)) {
      return false;
    }
    return true; 
  },//验证邮箱
  getRequestUrl: 'https://www.tao2.xyz/ZXAppService.svc',//请求地址
  getUrl(route) {
    return `https://www.tao2.xyz/ZXAppService.svc/${route}`;
  }//请求接口
  , getImgUrl: `https://www.tao2.xyz/ImgUpload.ashx`

  // getRequestUrl: 'https://www.zjecht.cn/ZXAppService.svc',//请求地址
  // getUrl(route) {
  //   return `https://www.zjecht.cn/ZXAppService.svc/${route}`;
  // }//请求接口
  // , getImgUrl: `https://www.zjecht.cn/ImgUpload.ashx`
  
  // getRequestUrl: 'http://localhost:9223/ZXAppService.svc',//请求地址
  // getUrl(route) {
  //   return `http://localhost:9223/ZXAppService.svc/${route}`;
  // }//请求接口
  // , getImgUrl: `http://localhost:9223/ImgUpload.ashx`
})