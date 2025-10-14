INCLUDE globals.ink

+{haveGetFlyer != "true"}->noflyer
+{haveGetFlyer == "true" and CatGetFlyer!= "true"}->haveflyer
+{CatGetFlyer == "true"}->alreadygetflyer

===noflyer===
#speaker:default #portrait:default 
喵喵喵喵喵。
->END

===haveflyer===
#speaker:default #portrait:default 
喵喵喵喵喵。
+[給傳單]->giveflyer
+[不給傳單]->dontgiveflyer

===dontgiveflyer===
#speaker:lucid #portrait:lucid
喵喵喵。
#speaker:default #portrait:default 
？(什麼叫你要把我的毛拔光)
->END

===giveflyer===
~ CatGetFlyer ="true"
#speaker:lucid #portrait:lucid
小貓怎麼辦……芙洛爾失蹤了……。
#speaker:default #portrait:default 
喵喵。
#speaker:lucid #portrait:lucid
芙洛爾的媽媽請我幫忙發傳單，這張就給你了，我貼在旁邊。
#speaker:default #portrait:default 
喵喵喵喵喵喵喵喵。
->END

===alreadygetflyer===
喵喵喵喵。
->END
