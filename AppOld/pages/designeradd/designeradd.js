var app = getApp();
var ite = 4;
var imgcode = 0;
Page({
  data: {
    imgcount: 3,
    imgdefault: '/images/addimg.png',
    CaseDesc: '', //作品介绍
    ImgTitle: '', //图片简介
    ImageFile: [{
      src: '/images/addimg.png',
      id: 0,
      hid: false,
      upload: 'uploadworks'
    }], //作品主图
    DeName: '', //姓名
    Sex: 0, //性别
    WorkYear: 0, //从事年数
    School: '', //毕业学校
    Experience: '', //工作经历
    Idea: '', //设计理念
    Price: 0, //设计定价
    Mobile: '', //电话
    WxCode: '', //微信号
    Email: '' //邮箱
  },
  //文本框判断
  DeName: function(e) {
    this.setData({
      DeName: e.detail.value
    })
  },
  sexChange: function(e) {
    this.setData({
      Sex: e.detail.value
    })
  },
  WorkYear: function(e) {
    this.setData({
      WorkYear: e.detail.value
    })
  },
  School: function(e) {
    this.setData({
      School: e.detail.value
    })
  },
  Experience: function(e) {
    this.setData({
      Experience: e.detail.value
    })
  },
  Idea: function(e) {
    this.setData({
      Idea: e.detail.value
    })
  },
  Price: function(e) {
    this.setData({
      Price: e.detail.value
    })
  },
  CaseDesc: function(e) {
    this.setData({
      CaseDesc: e.detail.value
    })
  },
  ImgTitle: function(e) {
    this.setData({
      ImgTitle: e.detail.value
    })
  },
  Mobile: function(e) {
    this.setData({
      Mobile: e.detail.value
    })
  },
  WxCode: function(e) {
    this.setData({
      WxCode: e.detail.value
    })
  },
  Email: function(e) {
    this.setData({
      Email: e.detail.value
    })
  },
  uploadworks: function(e) {
    const that = this;
    var length;
    var rest;
    var fielpaths;
    wx.chooseImage({　　
      count: 4, //最多可以选择的图片数，默认为9
      　　sizeType: ['orignal', 'compressed'], //original 原图，compressed 压缩图，默认二者都有
      　　sourceType: ['album', 'camera'], //album 从相册选图，camera 使用相机，默认二者都有
      　　success: function(res) {
        length = res.tempFilePaths.length;
        fielpaths = res.tempFilePaths
        var i = 0; //第几个
        that.uploadDIY(length, res, i, fielpaths);

      }, //成功则返回图片的本地文件路径列表 tempFilePaths
      // 　　fail: function () { }, //接口调用失败的回调函数
      // 　　complete: function () {
      //  } //接口调用结束的回调函数（调用成功、失败都会执行）
    })
  },
  uploadDIY: function(length, res, i, fielpaths) {
    const that = this;
    var imglist = that.data.ImageFile;
    var fielpathst = fielpaths;
    wx.uploadFile({
      url: app.getImgUrl, //开发者服务器 url
      filePath: fielpaths[i], //要上传文件资源的路径
      name: 'file', //文件对应的 key , 开发者在服务器端通过这个 key 可以获取到文件二进制内容

      success: function(res) {
        var imgobj = {
          src: '',
          id: null,
          hid: null
        };
        var result = JSON.parse(res.data);
        if (result.IsSuccess) {
          imgobj.src = result.ResultInfo.servicePath;
          imgobj.id = i;
          imgobj.hid = false;
          imglist.unshift(imgobj);
          that.setData({
            ImageFile: imglist,
          })
        } else {
          wx.showModal({
            title: '提示',
            content: result.Message,
            showCancel: false,
            success: function(res) {
              if (res.confirm) {
                wx.navigateBack({
                  delta: 1
                })
              }
            }
          })
        }
      },
      complete: () => {
        imgcode = imgcode + 1;
        i++;
        if (i != length) {
          that.uploadDIY(length, res, i, fielpaths)
        }
        if (imgcode == ite) {
          imglist[ite].hid = true;
          that.setData({
            ImageFile: imglist
          })
        }
      }
    })
  },
  add: function() {
    const that = this;

    if (that.data.DeName == "") {
      app.showerror("姓名不能为空");
      return;
    }
    if (that.data.WorkYear == "") {
      app.showerror("从事年数不能为空");
      return;
    }
    if (!app.IsInt(that.data.WorkYear)) {
      app.showerror("从事年数必须为正整数");
      return;
    }
    if (that.data.School == "") {
      app.showerror("毕业学校不能为空");
      return;
    }
    if (that.data.Experience == "") {
      app.showerror("工作经历不能为空");
      return;
    }
    if (that.data.Idea == "") {
      app.showerror("设计理念不能为空");
      return;
    }
    if (that.data.Price == "") {
      app.showerror("设计定价不能为空");
      return;
    }
    if (!app.IsInt(that.data.Price)) {
      app.showerror("设计定价必须为正整数");
      return;
    }
    //if (that.data.ImgTitle == "") {
    //  app.showerror("设计作品不能为空");
    //  return;
    // }
    var ImageFileList = new Array();
    var headPhoto="";
    for (var i = 0; i < that.data.ImageFile.length; i++) {
      if (that.data.ImageFile[i].src != that.data.imgdefault) {
        var img = {
          ImageFile: that.data.ImageFile[i].src,
          ImageTitle: "title" + that.data.ImageFile[i].id
        }
        ImageFileList.push(img);
        headPhoto=img.ImageFile;
      }
    }
    if (ImageFileList.length <= 0) {
      app.showerror("请上传设计作品");
      return;
    }
    //if (that.data.CaseDesc == "") {
    //  app.showerror("作品介绍不能为空");
    //   return;
    //  }
    if (that.data.Mobile == "") {
      app.showerror("电话不能为空");
      return;
    }
    if (!app.IsPhone(that.data.Mobile)) {
      app.showerror("电话格式不对");
      return;
    }
    if (that.data.WxCode == "") {
      app.showerror("微信号不能为空");
      return;
    }

    // if (that.data.Email == "") {
    //   app.showerror("邮箱不能为空");
    //   return;
    // }
    // if (!app.IsEmail(that.data.Email)) {
    //   app.showerror("邮箱格式不对");
    //   return;
    //  }
    wx.request({
      url: app.getUrl("AddDesigner"),
      data: {
        CaseDesc: that.data.CaseDesc, //作品介绍
        ImgTitle: that.data.ImgTitle, //图片简介
        ImageFileList: ImageFileList, //作品主图
        DeName: that.data.DeName, //姓名
        Sex: that.data.Sex, //性别
        WorkYear: that.data.WorkYear, //从事年数
        School: that.data.School, //毕业学校
        Experience: that.data.Experience, //工作经历
        Idea: that.data.Idea, //设计理念
        Price: that.data.Price, //设计定价
        Mobile: that.data.Mobile, //电话
        WxCode: that.data.WxCode, //微信号
        AreaID: app.globalData.AreaInfoData.cityID,
        Photo: headPhoto,
        //Email: that.data.Email,//邮箱
        ExistId: null
      },
      method: "POST",
      success: function(rnt) {
        var result = JSON.parse(rnt.data);
        if (result.IsSuccess) {
          wx.showModal({
            title: '提示',
            content: '添加成功',
            showCancel: false,
            success: function(res) {
              if (res.confirm) {
                var toUrl = '../home/home';
                wx.reLaunch({
                  url: toUrl,
                })
              }
            }
          })

        } else {
          wx.showModal({
            title: '提示',
            content: result.Message,
            showCancel: false,
            success: function(res) {
              if (res.confirm) {
                wx.navigateBack({
                  delta: 1
                })
              }
            }
          })
        }
      }
    })
  }
})