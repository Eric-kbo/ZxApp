var app = getApp();
Page({
data:{
  Url:''
},
onLoad: function (options){
  const that = this;
  var urlto=options.url;
  this.setData({
    Url: urlto
  })
  }
  
})