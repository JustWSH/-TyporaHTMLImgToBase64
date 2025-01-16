**使用其他语言阅读 | Read this in other languages: [English](README_en.md), [中文](README_zh.md).**



这是一个搭配**Typora.exe**使用的命令行应用。主要解决的问题是：

- Typora.exe导出的HTML文件如果含有本地图片，图片会存在当都的文件夹内，不会包含在HTML内，导致无法做到单文件分享。

- 由于安全策略的原因，Edge无法成功的把包含本地文件的HTML转换为MHTML文件。

- 此应用可以在Typora.exe导出HTML时自动把**本地**图片转换为Base64格式包含在HTML文件内。
  
  

# 命令行功能：

## 查看当前软件版本

```bash
C:\Users\username>"C:\Program Files\Typora\extension\TyporaHTMLImgToBase64.exe" -v
v1
```

## 转换HTML文件

```bash
C:\Users\nicef>"C:\Program Files\Typora\extension\TyporaHTMLImgToBase64.exe" "{html }"
Converted. Output to:
C:\Users\nicef\Downloads\test\Docker.html
```
