INCLUDE globals.ink

+{haveGetFlyer != "true"}->noflyer
+{haveGetFlyer == "true" and ChickencakeOwnerFriendGetFlyer!= "true"}->haveflyer
+{ChickencakeOwnerFriendGetFlyer == "true"}->alreadygetflyer

===noflyer===
#speaker:default #portrait:default 
哎呦，路西安啊，真巧啊在這裡遇到你。
#speaker:lucid #portrait:lucid
你好。
->END

===haveflyer===
#speaker:default #portrait:default 
哎呦，路西安啊，真巧啊在這裡遇到你，你手上抱著的是甚麼啊？
+[給傳單]->giveflyer
+[不給傳單]->dontgiveflyer

===dontgiveflyer===
#speaker:lucid #portrait:lucid
嗯……有點事，不太好說。
#speaker:default #portrait:default 
好吧，掰掰啦。
->END

===giveflyer===
~ ChickencakeOwnerFriendGetFlyer ="true"
#speaker:lucid #portrait:lucid
您好，這是尋人啟事。
#speaker:default #portrait:default 
這……是芙洛爾喔？
#speaker:lucid #portrait:lucid
嗯，他突然失蹤了，他媽媽擔心的不得了。
#speaker:default #portrait:default 
我會跟阿雞師一起找的，希望他只是去躲個清靜，不要有事才好。
#speaker:lucid #portrait:lucid
真的嗎，謝謝你。
->END

===alreadygetflyer===
我們目前沒有他的消息，別氣餒，一定會找到的。
->END
