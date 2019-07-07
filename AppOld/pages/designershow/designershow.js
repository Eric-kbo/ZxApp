var config = require("../../utils/config.js");
var app = getApp();
Page({
  data: {
    DeID: '',
    DeLevel: 0,
    DeName: '',
    ClickCount: null,
    ClinchCount: null,
    Design: '',
    one: [{
      "linkType": 8,
      "link": "ewr",
      "title": "",
      "pic": "/images/homesy1.jpg",
      "showtitle": "dsf"
    }, {
      "linkType": 0,
      "link": "#",
      "pc_link": "#",
      "title": "导航名称",
      "pic": "/images/homesy.jpg"
    }, {
      "linkType": 0,
      "link": "#",
      "pc_link": "#",
      "title": "导航名称",
      "pic": "/images/homesy.jpg"
    }]
  },
  onLoad: function(options) {
    var DeID = options.DeID;
    const that = this;
    that.setData({
      DeID: options.DeID
      // 'BEACBF7D-CC7F-41BD-9344-354F9E0261AA'
      // ID: options.DeID,
    })
    that.loadData(that);
  },
  loadData(that) {
    console.info(this);
    var kao = this;
    wx.request({
      url: app.getUrl("GetDescImageList"),
      data: {
        DeID: that.data.DeID
      },
      method: "POST",
      success: function(rnt) {
        var result = JSON.parse(rnt.data);
        if (result.IsSuccess) {

          kao.setData({
            DeLevel: result.ResultInfo.DeLevel,
            DeName: result.ResultInfo.DeName,
            ClickCount: result.ResultInfo.ClickCount,
            ClinchCount: result.ResultInfo.ClinchCount,
            DeCaseList: result.ResultInfo.DeCaseList,
            Design: result.ResultInfo.DeCaseList[0].CaseDesc
          })
        } else {
          wx.showModal({
            title: '提示',
            content: result.Message,
            showCancel: false,
            success: function(res) {
              if (res.confirm) {
                //wx.navigateBack({ delta: 1 })
              }
            }
          })
        }
      }
    })
  },
  bindintrodu: function(e) {
    const dindtru = this;
    var idx = e.detail.current;
    dindtru.setData({
      Design: dindtru.data.DeCaseList[idx].CaseDesc
    })
  }
})