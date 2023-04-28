# Sharp Alternative Code Execution



## 项目简介

C#版本的AlternativeShellcodeExec：https://github.com/aahmad097/AlternativeShellcodeExec

详细文章：https://yzddmr6.com/posts/SharpAlternativeShellcodeExec/

开发环境：

* JetBrains Rider
* .NET Framework 4.6

## 项目原理

利用ShellCode进行免杀是一种较为流行的免杀方式，但是常见的VirtualAlloc、CreateRemoteThread这些Windows API已经被各大杀软重点监控。那么与之相对的绕过的办法就是利用一些小众的Windows API，这些API函数往往提供了回调的功能，当它的参数是指针类型的话就可以直接执行内存当中的ShellCode，这样就达到了绕过敏感函数识别执行ShellCode的目的。



## 为什么要用C#重写

* Windows XP以来每台Windows上都默认安装了.NET Framework，C#又天生支持内存加载，在无文件攻击场景下十分方便。
* 经过测试，同样的硬编码ShellCode+裸API调用，C#重写后的版本也比原版本少20个左右引擎的检出。
* 学习练手，加深对各类回调函数免杀的理解，顺便实践一下ChatGPT的应用。



## 关于ChatGPT

C#在调用Windows API的时候需要额外进行函数的声明，并且要实现函数中C++类型到C#类型的映射转化。这部分工作十分的繁琐，就借助了ChatGPT去做转化，帮我减少了至少一半的工作量。 ~~失业警告~~



## 简单测试

原C++版本，硬编码ShellCode+裸API调用

![img](https://cdn.nlark.com/yuque/0/2023/png/1599908/1682324049390-04a0c9ed-34c1-4431-b90d-01a8666cd468.png)

C#重写版本，硬编码ShellCode+裸API调用

![img](https://cdn.nlark.com/yuque/0/2023/png/1599908/1682324077632-bd2ac178-46ef-4f6e-bf41-c615577f75a1.png)

稍微混淆一下，就能绕过一些杀软

![img](https://cdn.nlark.com/yuque/0/2023/png/1599908/1682061253916-feea7c82-3832-41f0-bc0c-f113223f489a.png)



![img](https://cdn.nlark.com/yuque/0/2023/png/1599908/1682064903931-bee70de3-c95d-42ba-8874-6244ad4c9a61.png)

## 更新日志

### 2023.4.25 

* 重写原项目45种回调方式，除FiberContextEdit方式外均可正常运行



## 注意事项

本插件仅供合法的渗透测试以及爱好者参考学习，请勿用于非法用途，否则自行承担相关责任。