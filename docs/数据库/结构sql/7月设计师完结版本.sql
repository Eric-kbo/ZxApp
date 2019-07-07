/*
Navicat SQL Server Data Transfer

Source Server         : 天杰装修项目小程序数据库
Source Server Version : 110000
Source Host           : 120.26.60.55:1433
Source Database       : ZX
Source Schema         : dbo

Target Server Type    : SQL Server
Target Server Version : 110000
File Encoding         : 65001

Date: 2018-07-18 10:14:31
*/


-- ----------------------------
-- Table structure for ZX_AD
-- ----------------------------
DROP TABLE [ZX_AD]
GO
CREATE TABLE [ZX_AD] (
[ID] uniqueidentifier NOT NULL DEFAULT (newid()) ,
[Position] int NULL ,
[AdName] nvarchar(20) NULL ,
[Http] nvarchar(100) NULL ,
[Enable] bit NULL DEFAULT ((0)) ,
[BeginTime] datetime NULL ,
[EndTime] datetime NULL ,
[CreateTime] datetime NULL DEFAULT (getdate()) ,
[VisitCount] int NULL ,
[Remark] nvarchar(100) NULL ,
[ImageFile] nvarchar(100) NULL 
)


GO

-- ----------------------------
-- Table structure for ZX_Area
-- ----------------------------
DROP TABLE [ZX_Area]
GO
CREATE TABLE [ZX_Area] (
[ID] uniqueidentifier NOT NULL DEFAULT (newid()) ,
[FID] uniqueidentifier NULL ,
[AreaName] varchar(20) NULL ,
[SortNo] int NULL 
)


GO

-- ----------------------------
-- Table structure for ZX_DeCase
-- ----------------------------
DROP TABLE [ZX_DeCase]
GO
CREATE TABLE [ZX_DeCase] (
[ID] uniqueidentifier NOT NULL DEFAULT (newid()) ,
[DesignerID] uniqueidentifier NULL ,
[CaseTitle] nvarchar(50) NULL ,
[CaseDesc] nvarchar(500) NULL ,
[ImageFile] nvarchar(500) NULL ,
[UpdateTime] datetime NULL DEFAULT (getdate()) ,
[SortNo] int NULL 
)


GO

-- ----------------------------
-- Table structure for ZX_Designer
-- ----------------------------
DROP TABLE [ZX_Designer]
GO
CREATE TABLE [ZX_Designer] (
[ID] uniqueidentifier NOT NULL DEFAULT (newid()) ,
[AreaID] uniqueidentifier NULL ,
[DeName] nvarchar(20) NULL ,
[Sex] bit NULL ,
[WorkYear] int NULL ,
[School] nvarchar(30) NULL ,
[Experience] nvarchar(500) NULL ,
[Idea] nvarchar(200) NULL ,
[Eexpert] nvarchar(20) NULL ,
[Price] int NULL ,
[Mobile] nvarchar(11) NULL ,
[WxCode] nvarchar(50) NULL ,
[Email] nvarchar(50) NULL ,
[ClinchCount] int NULL ,
[ClickCount] int NULL ,
[CheckState] nvarchar(10) NULL ,
[CheckDesc] nvarchar(50) NULL ,
[ExistId] nvarchar(255) NULL ,
[OrderCount] int NULL ,
[DesignerTitle] nvarchar(20) NULL ,
[DeLevel] int NULL ,
[PreCount] int NULL ,
[Photo] varchar(255) NULL 
)


GO

-- ----------------------------
-- Table structure for ZX_DesignStyle
-- ----------------------------
DROP TABLE [ZX_DesignStyle]
GO
CREATE TABLE [ZX_DesignStyle] (
[ID] uniqueidentifier NOT NULL DEFAULT (newid()) ,
[StyleType] int NULL ,
[ImageFile] nvarchar(100) NULL ,
[ImageDesc] nvarchar(200) NULL ,
[VisitCount] int NULL ,
[Enable] bit NULL DEFAULT ((0)) ,
[Remark] nvarchar(100) NULL ,
[LastTime] datetime NULL DEFAULT (getdate()) 
)


GO

-- ----------------------------
-- Table structure for ZX_ShopActive
-- ----------------------------
DROP TABLE [ZX_ShopActive]
GO
CREATE TABLE [ZX_ShopActive] (
[ID] uniqueidentifier NOT NULL ,
[ActiveDesc] nvarchar(1000) NULL ,
[ShopID] uniqueidentifier NULL ,
[ActiveName] nvarchar(30) NULL ,
[BeginTime] datetime NULL ,
[EndTime] datetime NULL ,
[Enabled] bit NULL 
)


GO

-- ----------------------------
-- Table structure for ZX_ShopWare
-- ----------------------------
DROP TABLE [ZX_ShopWare]
GO
CREATE TABLE [ZX_ShopWare] (
[ID] uniqueidentifier NOT NULL ,
[ShopID] uniqueidentifier NULL ,
[WareName] nvarchar(20) NULL ,
[WareDesc] nvarchar(100) NULL ,
[Brand] nvarchar(20) NULL ,
[Model] nvarchar(20) NULL ,
[Unit] nvarchar(10) NULL ,
[Specification] nvarchar(20) NULL ,
[OldPrice] decimal(18,2) NULL ,
[NewPrice] decimal(18,2) NULL ,
[BeginTime] datetime NULL ,
[EndTime] datetime NULL ,
[Enabled] bit NULL DEFAULT ((0)) ,
[LastTime] datetime NULL DEFAULT (getdate()) ,
[AloneSell] bit NULL 
)


GO

-- ----------------------------
-- Table structure for ZX_User
-- ----------------------------
DROP TABLE [ZX_User]
GO
CREATE TABLE [ZX_User] (
[ID] uniqueidentifier NOT NULL DEFAULT (newid()) ,
[UserType] int NULL ,
[LoginName] varchar(20) NULL ,
[FName] varchar(20) NULL ,
[PW] varchar(30) NULL ,
[Email] varchar(20) NULL ,
[Mobile] varchar(11) NULL ,
[Remark] varchar(100) NULL ,
[CreateTime] datetime NULL DEFAULT (getdate()) ,
[UpdateTime] datetime NULL DEFAULT (getdate()) ,
[AreaID] uniqueidentifier NULL ,
[WxID] nvarchar(60) NULL 
)


GO

-- ----------------------------
-- Table structure for ZX_WebPage
-- ----------------------------
DROP TABLE [ZX_WebPage]
GO
CREATE TABLE [ZX_WebPage] (
[ID] uniqueidentifier NOT NULL DEFAULT (newid()) ,
[ContentType] int NULL ,
[Title] nvarchar(50) NULL ,
[ContentDesc] nvarchar(100) NULL ,
[ImageFile] nvarchar(100) NULL ,
[Http] nvarchar(100) NULL ,
[Enable] bit NULL DEFAULT ((0)) ,
[CreateTime] datetime NULL DEFAULT (getdate()) ,
[VisitCount] int NULL 
)


GO

-- ----------------------------
-- Indexes structure for table ZX_AD
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table ZX_AD
-- ----------------------------
ALTER TABLE [ZX_AD] ADD PRIMARY KEY ([ID])
GO

-- ----------------------------
-- Indexes structure for table ZX_Area
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table ZX_Area
-- ----------------------------
ALTER TABLE [ZX_Area] ADD PRIMARY KEY ([ID])
GO

-- ----------------------------
-- Indexes structure for table ZX_DeCase
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table ZX_DeCase
-- ----------------------------
ALTER TABLE [ZX_DeCase] ADD PRIMARY KEY ([ID])
GO

-- ----------------------------
-- Indexes structure for table ZX_Designer
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table ZX_Designer
-- ----------------------------
ALTER TABLE [ZX_Designer] ADD PRIMARY KEY ([ID])
GO

-- ----------------------------
-- Indexes structure for table ZX_DesignStyle
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table ZX_DesignStyle
-- ----------------------------
ALTER TABLE [ZX_DesignStyle] ADD PRIMARY KEY ([ID])
GO

-- ----------------------------
-- Indexes structure for table ZX_ShopActive
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table ZX_ShopActive
-- ----------------------------
ALTER TABLE [ZX_ShopActive] ADD PRIMARY KEY ([ID])
GO

-- ----------------------------
-- Indexes structure for table ZX_ShopWare
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table ZX_ShopWare
-- ----------------------------
ALTER TABLE [ZX_ShopWare] ADD PRIMARY KEY ([ID])
GO

-- ----------------------------
-- Indexes structure for table ZX_User
-- ----------------------------
CREATE UNIQUE INDEX [IX_ZX_User] ON [ZX_User]
([LoginName] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO

-- ----------------------------
-- Primary Key structure for table ZX_User
-- ----------------------------
ALTER TABLE [ZX_User] ADD PRIMARY KEY ([ID])
GO

-- ----------------------------
-- Indexes structure for table ZX_WebPage
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table ZX_WebPage
-- ----------------------------
ALTER TABLE [ZX_WebPage] ADD PRIMARY KEY ([ID])
GO

-- ----------------------------
-- Foreign Key structure for table [ZX_DeCase]
-- ----------------------------
ALTER TABLE [ZX_DeCase] ADD FOREIGN KEY ([DesignerID]) REFERENCES [ZX_Designer] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
GO

-- ----------------------------
-- Foreign Key structure for table [ZX_Designer]
-- ----------------------------
ALTER TABLE [ZX_Designer] ADD FOREIGN KEY ([AreaID]) REFERENCES [ZX_Area] ([ID]) ON DELETE SET NULL ON UPDATE CASCADE
GO

-- ----------------------------
-- Foreign Key structure for table [ZX_User]
-- ----------------------------
ALTER TABLE [ZX_User] ADD FOREIGN KEY ([AreaID]) REFERENCES [ZX_Area] ([ID]) ON DELETE SET NULL ON UPDATE CASCADE
GO
