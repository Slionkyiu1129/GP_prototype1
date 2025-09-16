INCLUDE globals.ink

+{haveGetFlyer != "true"}->noflyer
+{haveGetFlyer == "true" and FlowerSonGetFlyer!= "true"}->haveflyer
+{FlowerSonGetFlyer == "true"}->alreadygetflyer

===noflyer===
#speaker:default #portrait:default 
欸，路西安？你也來看海？
#speaker:lucid #portrait:lucid
我只是路過啦。
->END

===haveflyer===
#speaker:default #portrait:default 
路西安？有什麼事嗎？
+[給傳單]->giveflyer
+[不給傳單]->dontgiveflyer

===dontgiveflyer===
#speaker:lucid #portrait:lucid
我來找點東西。
#speaker:default #portrait:default 
掰掰啦。
->END

===giveflyer===
~ FlowerSonGetFlyer ="true"
#speaker:lucid #portrait:lucid
我來發尋人啟事的。
#speaker:default #portrait:default 
這個是……芙洛爾？他失蹤了？
#speaker:lucid #portrait:lucid
你有看到他嗎？
#speaker:default #portrait:default 
前幾天他確實有來找我，問了我很多有關花的事情。
#speaker:lucid #portrait:lucid
花？
#speaker:default #portrait:default 
一種島上不常見的品種，大概只有我家的花店有賣吧……不過，還是趕快照到人要緊吧！我會多注意的，希望能趕快找到人。
->END

===alreadygetflyer===
事情一定會解決的，如果你心情很差的話也來試試看海吧，大家總能在海風中找回平靜。
->END
