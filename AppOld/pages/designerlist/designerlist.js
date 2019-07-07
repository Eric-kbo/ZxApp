var app = getApp();
Page({
  data: {
    DesignerList: null,
    DesignerImgs: null,
    DesignerArticle: null,
    KeyWord: '',
    PageIndex: 1,
    PageSize: 6,
    Num: 0,
    SortClass: '',
    ADInfo: [
      { "linkType": 0, "link": "#", "pc_link": "#", "title": "导航名称", "pic": "/images/homesy1.jpg" }
    ]
  },
  onLoad: function (options) {

    // 页面初始化 options为页面跳转所带来的参数
    //var keyword = options.keyword;
    var keyword = options.keyword
    if (keyword == undefined) keyword = "";
    const that = this;
    that.loadData(that, false);

  },
  articleClick: function (e) {
     var url = e.currentTarget.dataset.link;
     var toUrl = '/pages/webpageshow/webpageshow?url=' + url;
     wx.navigateTo({
       url: toUrl,
     })
       },
  showimg: function (e) {
    var imgs = e.target.dataset.imgs;
  var old = new Array();
    for (var i = 0; i < imgs.length; i++) {

      var r = imgs[i].ImageFile;
      old.push(r);
    }
   wx.previewImage({
      current: old[0], // 当前显示图片的http链接
      urls: old // 需要预览的图片http链接列表
    })
  },
  onReady: function () {
    // 页面渲染完成
  },
  onShow: function () {
    var keyword = "";
    // 页面显示
    if (this.data.keyword == undefined) keyword = "";
    const that = this;
    that.setData({
      KeyWord: keyword,
      PageIndex: 1
    })
    that.loadData(that, false);
  },
  onHide: function () {
    // 页面隐藏
  },
  onUnload: function () {
    // 页面关闭
  },
  bindKeyWordInput: function (e) {
    this.setData({
      KeyWord: e.detail.value
    })
  },
  bindBlurInput: function (e) {
    wx.hideKeyboard()
  },
  goToDetail: function (e) {
    var id = e.currentTarget.dataset.id;
    var toUrl = '../designerdetail/designerdetail?id=' + id;
    wx.navigateTo({
      url: toUrl,
    })
  },
  loadData(that, isNextPage) {
    //广告图
    wx.request({
      url: app.getUrl("SelADEntity"),
      data: { "Position": 2 },
      method: "POST",
      success: function (rnt) {
        var result = JSON.parse(rnt.data);

        if (result.ResultInfo.length > 0) {
          var b = []; //用来接收广告的对象
          for (var i = 0; i < result.ResultInfo.length; i++) {
            var a = {};
            a.linkType = 8;
            a.link = result.ResultInfo[i].Http;
            a.title = result.ResultInfo[i].AdName;
            a.pic = result.ResultInfo[i].ImageFile;
            a.showtitle = "";
            b[i] = a;
          }
          that.setData({//将广告写入到data中的数据
            ADInfo: b
        })
      }

      }
    })
    wx.request({
      url: app.getUrl("SelDes"),
      data:
       //{ "AreaName": "上海", "AreaID": "2A5373CE-BF41-4A8D-94F6-AB1CD9FB2CB1" },
      //  { "AreaName": "长沙", "AreaID": "71D0C17C-4A50-4F35-9750-F8FCAD3DA4AB" },
       { "AreaName": app.globalData.AreaInfoData.city, "AreaID": app.globalData.AreaInfoData.cityID },
      method: "POST",
        success: function (rnt) {
         var result  = JSON.parse(rnt.data);
        if (result.IsSuccess) {
            var r = result.ResultInfo;
            //if (isNextPage) {
            //  var old = that.data.DesignerList;
            //  old.push.apply(old, r);
            //  that.setData({
           //     DesignerList: old
           //  })
          //  }
          //else {
          that.setData({
            DesignerList: r
          })
          // }
          wx.hideNavigationBarLoading();
        }
        else {
          wx.showModal({
            title: '提示',
            content: result.Message,
            showCancel: false,
            success: function (res) {
              if (res.confirm) {
                // wx.navigateBack({ delta: 1 })
              }
            }
         })
          }
       }
      })
    //设计风格
     wx.request({
       url: app.getUrl("SelectDesignStyle"),
       method: "POST",
       success: function (rnt) {

         var result = JSON.parse(rnt.data);
         that.setData({
           DesignerImgs: result.ResultInfo
         })
       }
     })
     //装修风格
     wx.request({
       url: app.getUrl("WebpageList"),
       method: "POST",
       data: {
         "ContentType": 1,
         "PageIndex": 1,
         "PageSize": 2
        },
         success: function (rnt) {
           var result = JSON.parse(rnt.data);
           that.setData({
             DesignerArticle: result.ResultInfo
           })
         }
  })
  }
})