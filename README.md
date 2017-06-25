# FormShadow for .NET Winform

[\[中文版README\]](https://github.com/NetDimension/Winform-FormShadow/wiki/%E5%85%B3%E4%BA%8E%E6%9C%AC%E9%A1%B9%E7%9B%AE)

__FormShadow__ is a tiny library can make __dropshadows__ for .NET winfroms. Of course, it isn't just add __CS_DROPSHADOW__ to window style. 

It makes 4 LayeredWindow with shadow image around parent window (like latest Visual Studio Shell does).

![Screen Shot](http://www.ohtrip.cn/media/FormShadowDecorator.png)


# Features
- Make beautiful WinForm dropshadows with __ACTIVE__/__NOACTIVE__ styles support.
- Fast draw dropshadows around the target window.

# Examples
This example defines a decorator for main window(Borderless), and make dropshadows around the window. Set __EnableResize__ to __true__ , then you can resize the main window by drag these shadows.

```C#
public partial class Form1 : Form
{

	protected readonly FormShadowDecorator ShadowDecorator;

	public Form1()
	{
		InitializeComponent();
		ShadowDecorator = new FormShadowDecorator(this);
		//启用窗口大小调整
		//Enable resizing form with shadows.
		ShadowDecorator.EnableResize(true);
	}
}
```


# Discussion
Join the QQ group or add my QQ contact to discuss all about Microsoft .NET technology.

__QQ Group:__ 241088256

__My QQ Account:__ 19843266

__BLOG:__ [博客园](http://www.cnblogs.com/linxuanchen/)

# Donate
If you like my work, please buy me a cup of coffee to encourage me continue with this library. 
![Screen Shot](http://www.ohtrip.cn/media/BEG.png)

