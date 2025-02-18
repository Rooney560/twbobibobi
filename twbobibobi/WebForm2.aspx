<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="twbobibobi.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        
        #toc-container {
            background: #f9f9f9;
            border: 1px solid #aaa;
            border-radius: 4px;
            -webkit-box-shadow: 0 1px 1px rgba(0, 0, 0, .05);
            box-shadow: 0 1px 1px rgba(0, 0, 0, .05);
            display: table;
            margin-bottom: 1em;
            padding: 10px 20px 10px 10px;
            position: relative;
            width: auto;
        }
        .toc-container-direction {
            direction: ltr;
        }
        .toc-title-container {
            display: table;
            width: 100%;
        }
        div#toc-container .toc-title {
            font-weight: 500;
        }

        div#toc-container .toc-title {
            font-size: 120%;
        }

        div#toc-container .toc-title {
            display: initial;
        }

        #toc-container .toc-title {
            text-align: left;
            line-height: 1.45;
            margin: 0;
            padding: 0;
        }

        .toc-counter ul {
    counter-reset: item;
}

        #toc-container ul, #toc-container li {
            background: 0 0;
            list-style: none;
            list-style-position: initial;
            list-style-image: initial;
            list-style-type: none;
            line-height: 1.6;
            margin: 0;
            overflow: hidden;
        }
        
        #toc-container ul, #toc-container li {
            padding: 0;
        }

        div#toc-container ul li {
            font-weight: 500;
        }

        div#toc-container ul li {
            font-size: 95%;
        }

        #toc-container a {
            color: #444;
            box-shadow: none;
            text-decoration: none;
            text-shadow: none;
            display: inline-flex;
            align-items: stretch;
            flex-wrap: nowrap;
        }
        
        #toc-container ul {
            list-style-type: disc;
            list-style-position: inside;
        }
        
        #toc-container ol, #toc-container ul {
            padding-left: 1rem;
            margin-left: 1rem;
        }

        .toc-counter nav ul li a:before {
            content: counters(item, '.', decimal) '. ';
            display: inline-block;
            counter-increment: item;
            flex-grow: 0;
            flex-shrink: 0;
            margin-right: .2em;
            float: left;
        }

        #toc-container ol ul, #toc-container ul ul {
            list-style-type: circle;
            list-style-position: inside;
            margin-left: 15px;
        }

        #toc-container a:visited {
            color: #9f9f9f;
        }

        #toc-container a:hover {
    text-decoration: underline;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
                        <div id="toc-container" class="toc-container-direction toc-counter">
                            <div class="toc-title-container">
                                <p class="toc-title" style="cursor:inherit">目錄</p>
                            </div>
                            <nav>
                                <ul>
                                    <li>
                                        <a class="toc-link toc-heading-1" href="#">2024 下半年神卡必備 6 張信用卡推薦！</a>
                                        2025犯太歲生肖有哪些?</li>
                                    <ul>
                                        <li class="second">
                                        <a class="toc-link toc-heading-1" href="#">2024 下半年神卡必備 6 張信用卡推薦！</a>化解太歲的方法</li>
                                        <li class="second">
                                            <a class="toc-link toc-heading-1" href="#">
                                                安太歲象徵的意義與原因</a></li>
                                    </ul>
                                    <li><a href="Lights03.aspx" target="_blank">光明燈介紹</a></li>
                                    <li><a href="Lights04.aspx" target="_blank">太歲燈介紹</a></li>
                                    <li><a href="Lights06.aspx" target="_blank">財神燈介紹</a></li>
                                    <li>姻緣燈介紹</li>
                                    <li>文昌燈介紹</li>
                                    <li>貴人燈介紹</li>
                                    <li>龍王燈介紹</li>
                                    <li><a href="Lights14.aspx" target="_blank">虎爺燈介紹</a></li>
                                    <li>福壽燈介紹</li>
                                    <li>寵物平安燈介紹</li>
                                    <li>月老桃花燈介紹</li>
                                    <li>藥師佛燈介紹</li>
                                    <li>觀音佛祖燈介紹</li>
                                </ul>
                            </nav>
                        </div>
    </form>
</body>
</html>
