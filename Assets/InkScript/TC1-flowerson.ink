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
前幾天他確實有來找我，要我賣給他一種很罕見的花。
#speaker:lucid #portrait:lucid
你還記得是哪種花嗎？
#speaker:default #portrait:default 
那花不常見，只有我爸的花店有賣。不過，芙洛爾叫我保密，我不知道該不該告訴你。
->END

===alreadygetflyer===
事情一定會解決的，如果你心情很差的話就來看看海吧。
->END
