var app = getApp();
Page({
  data: {
    
    ID:"",
    DeName: '', //姓名
    DesignerTitle: '', //设计师标题
    Eexpert: '', //擅长空间
    Idea: '', //设计理念
    Price: null, //价格
    CaseList: null, //设计方案图片列表
    ResultInfolist: [],
    DesignerArticle:null,
    Photo: '',
    ADInfo: [
      { "linkType": 0, "link": "#", "pc_link": "#", "title": "导航名称", "pic": "/images/homesy1.jpg" }
    ]
  },
  onLoad: function(options) {
    var id = options.id;
    const that = this;
    console.log(id);
    that.setData({
     // ID: '251612C2-25B4-467B-B2D3-0D529C062FA7'
       ID: options.id
    })
    that.loadData(that);
  },
  loadData(that) {
    var kao = this;
    wx.request({
      url: app.getUrl("GetDesignerInfo"),
      data: {
        ID: that.data.ID
      },
      method: "POST",
      success: function(rnt) {
        var result = JSON.parse(rnt.data);
        if (result.IsSuccess) {
          console.info(result.ResultInfo.Photo)
          kao.setData({
            DesignerTitle: result.ResultInfo.DesignerTitle ? result.ResultInfo.DesignerTitle : "某某设计",
            DeName: result.ResultInfo.DeName,
            Price: result.ResultInfo.Price,
            Idea: result.ResultInfo.Idea,
            Eexpert: result.ResultInfo.Eexpert ? result.ResultInfo.Eexpert : "无",
            CaseList: result.ResultInfo.CaseList,
            Photo: result.ResultInfo.Photo
          })
        } else {
          wx.showModal({
            title: '提示',
            content: result.Message,
            showCancel: false,
            success: function(res) {
              if (res.confirm) {
              }
            }
          })
        }
      }
    });
    wx.request({
      url: app.getUrl("WebpageList"),
      method: "POST",
      data: {
        "ContentType": 2,
        "PageIndex": 1,
        "PageSize": 2
      },
      success: function (rnt) {
        var result = JSON.parse(rnt.data);
        that.setData({
          DesignerArticle: result.ResultInfo
        })
      }
    });
    //广告图
    wx.request({
      url: app.getUrl("SelADEntity"),
      data: { "Position": 1 },
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
    });
  },
  articleClick: function (e) {
    var url = e.currentTarget.dataset.link;
    var toUrl = '/pages/webpageshow/webpageshow?url=' + url;
    wx.navigateTo({
      url: toUrl,
    })
  },

  ImgShow:function(){
    var DeID=this.data.ID
    var toUrl = '../designershow/designershow?DeID=' + encodeURIComponent(DeID);
    console.info(toUrl)
    wx.navigateTo({
      url: toUrl,
    })
  }
})