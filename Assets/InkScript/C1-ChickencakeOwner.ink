INCLUDE globals.ink

+{haveGetFlyer != "true"}->noflyer
+{haveGetFlyer == "true" and ChickencakeOwnerGetFlyer!= "true"}->haveflyer
+{ChickencakeOwnerGetFlyer == "true"}->alreadygetflyer

===noflyer===
#speaker:default #portrait:default 
來喔～熱騰騰的雞蛋糕，今天有限量新口味唷！
#speaker:lucid #portrait:lucid
你好，我只是路過看看。
->END

===haveflyer===
#speaker:default #portrait:default 
哎呀，你的臉色不太對，是不是肚子餓啦？
+[給傳單]->giveflyer
+[不給傳單]->dontgiveflyer

===dontgiveflyer===
#speaker:lucid #portrait:lucid
我在找一個人，有點急。
#speaker:default #portrait:default 
找人？我每天只顧著攤子，能記住的只有雞蛋糕的配方啦～不然你買個雞蛋糕邊找邊吃？
->END

===giveflyer===
~ ChickencakeOwnerGetFlyer ="true"
#speaker:lucid #portrait:lucid
芙洛爾失蹤了，這是他的尋人傳單。您有看到他嗎？
#speaker:default #portrait:default 
嗯……
#speaker:default #portrait:default 
他前幾天好像真的有來過，站在這邊發呆。
#speaker:lucid #portrait:lucid
您記得他什麼時候來的嗎？。
#speaker:default #portrait:default 
好像是下午吧。
#speaker:lucid #portrait:lucid
謝謝你提供的資訊 !
->END

===alreadygetflyer===
你還在找芙洛爾啊，唉，希望他平安無事……
->END
