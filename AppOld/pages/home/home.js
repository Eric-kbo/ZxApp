var config = require("../../utils/config.js");
var app = getApp();
Page({
  data: {
    pageIndex: 1,
    pageSize: 10,
    RequestUrl: app.getRequestUrl,
    one: [{
      "linkType": 8,
      "link": "ewr",
      "title": "",
      "pic": "/images/homesy1.jpg",
      "showtitle": "dsf"
    }],
    two: [{
      "linkType": 0,
      "link": "#",
      "title": "导航名称",
      "pic": "/images/icons-1/changeface.png",
      "picHeight": 0
    }, {
      "linkType": 0,
      "link": "#",
      "title": "导航名称",
      "pic": "/images/icons-1/palette.png",
      "picHeight": 0
    }, {
      "linkType": 0,
      "link": "#",
      "title": "导航名称",
      "pic": "/images/icons-1/movie.png",
      "picHeight": 0
    }, {
      "linkType": 0,
      "link": "#",
      "title": "导航名称",
      "pic": "/images/icons-1/literature.png",
      "picHeight": 0
    }],
    three: [{
      "linkType": 0,
      "link": "../designeradd/designeradd",
      "title": "导航名称",
      "pic": "/images/icons-1/text.png",
      "picHeight": 0
    }, {
      "linkType": 0,
      "link": "../designerlist/designerlist",
      "title": "导航名称",
      "pic": "/images/icons-1/drama.png",
      "picHeight": 0
    }, {
      "linkType": 0,
      "link": "#",
      "title": "导航名称",
      "pic": "/images/icons-1/photgraphy.png",
      "picHeight": 0
    }, {
      "linkType": 0,
      "link": "#",
      "title": "导航名称",
      "pic": "/images/icons-1/film.png",
      "picHeight": 0
    }],
    four: [{
      "linkType": 0,
      "link": "#",
      "pc_link": "#",
      "title": "导航名称",
      "pic": "/images/homesy1.jpg",
      "picHeight": 320
    }]
  },
  onShow: function () {
    // this.GetShopCart();
  },
  onLoad: function (options) {
    // 生命周期函数--监听页面加载
    var that = this;
    that.loadData(that,false);

  },
  ClickSwiper: function (e) {
    var that = this;
    var urllink = e.currentTarget.dataset.link;
    var showtype = e.currentTarget.dataset.showtype;
    that.JumpUrlByType(showtype, urllink);
  },
  JumpUrlByType: function (typeId, urls) {
    switch (typeId) {
      case 10:
        wx.navigateToMiniProgram({
          appId: urls,
          extarData: {},
          envVersion: 'develop',
          success(res) {
            // 打开成功
            console.log("小程序跳转成功");
          }
        })
        break;
      case 23:
        wx.makePhoneCall({
          phoneNumber: urls //仅为示例，并非真实的电话号码
        })
        break;
      case 7:
        wx.switchTab({
          url: urls
        })
        break;
      case 3:
      case 4:
      case 8:
      case 9:
        var uu = app.getRequestUrl + urls;
        console.log(uu);
        wx.navigateTo({
          url: "../outurl/outurl?url=" + encodeURIComponent(uu)
        })
        break;
      default:
        wx.navigateTo({
          url: urls
        });
    }
  },

  //加载函数
  loadData(that, isNextPage) { 
    wx.request({
      url: app.getUrl("SelADEntity"),
      data: { "Position": 3 },
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
            one: b
          })
        }
      }
    })
  },

  //广告跳转页面功能
  articleClick: function (e) {
    var url = e.currentTarget.dataset.link;
    var toUrl = '/pages/webpageshow/webpageshow?url=' + url;
    wx.navigateTo({
      url: toUrl,
    })
  },
})