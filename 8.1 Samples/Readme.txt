プログラミングWindows 第6版

　本サンプルは、Programming Windows, 6th editionで公開されているC#サンプルを、原書の出版社である米国O'Reilly Media, Inc.の許可を得て、Windows 8.1環境へ移植したものです。オリジナルは、http://examples.oreilly.com/9780735671768-files/で公開されています。

動作確認をした環境は、以下の2種類です。
1) 日本語版Windows 8.1 Preview、日本語版Visual Studio 2013 Preview
2) 日本語版Windows 8.1、日本語版Visual Studio 2013 RC

章ごとのサブフォルダー（chxx）の中にテキストファイル（chxx.txt）が含まれている場合は、Windows 8.1へ移植する上で変更したプロジェクトと変更箇所のキーワードが含まれています。

移植の手順は、以下の方法を採用しました。
1. Visual Studio 2013で新規プロジェクトを作成。
2. コードとXAML定義を新規プロジェクトへ移動。
3. Windows 8.1に対応するコードに変更。
このような方法を採用した理由は、Visual Studio 2013のプロジェクト構造がVisual Studio 2012から変更されており、Visual Studio 2013の標準構造を採用するためです。


===============================================================================
Programming Windows, 6th edition for Japanese version.
--------------------------------

This sample migrated to Windows 8.1 environment from Programming Windows, 6th edition's sample, by permission of O'Reilly Media, Inc., which owns or controls all rights to publish and sell the same.  The original published at http://examples.oreilly.com/9780735671768-files/.


Tested is follow's environment.
1. Japanese Windows 8.1 Preview, Japanese Visual Studio 2013 Preview
1. Japanese Windows 8.1, Japanese Visual Studio 2013 RC


If the sub folder contain text file such as chxx.txt, it describe changed projects and changed points of project.


Migrate order is follow.
1. Create a new project using Visual Studio 2013.
2. Add sample's code and xaml to project.
3. Change code for Windows 8.1.

This reason is for minimizing the influence by change of project structure. 


