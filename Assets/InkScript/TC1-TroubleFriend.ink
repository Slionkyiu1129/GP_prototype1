INCLUDE globals.ink

+{haveGetFlyer != "true"}->noflyer
+{haveGetFlyer == "true" and TroubleFriendGetFlyer!= "true"}->haveflyer
+{TroubleFriendGetFlyer == "true"}->alreadygetflyer

===noflyer===
#speaker:default #portrait:default 
咦？你怎麼一副很忙的樣子，是在幹嘛啊？
#speaker:lucid #portrait:lucid
沒什麼，只是在找人。
#speaker:default #portrait:default 
哈哈，不會是又惹麻煩了吧？
->END

===haveflyer===
#speaker:default #portrait:default 
你今天神神祕祕的，是不是有什麼大秘密啊？
+[給傳單]->giveflyer
+[不給傳單]->dontgiveflyer

===dontgiveflyer===
#speaker:lucid #portrait:lucid
我在調查一件事。
#speaker:default #portrait:default 
哇～那我可要小心了。
->END

===giveflyer===
~ TroubleFriendGetFlyer ="true"
#speaker:lucid #portrait:lucid
這是芙洛爾的尋人啟事。你有看過他嗎？
#speaker:default #portrait:default 
這張傳單是什麼？失蹤？這麼嚴重？誰知道是不是自己躲起來了。
#speaker:lucid #portrait:lucid
這不是開玩笑的事。
#speaker:default #portrait:default 
好啦好啦，有什麼新消息我會告訴你……也許啦。
->END

===alreadygetflyer===
就說我會注意了，你煩不煩啊。
->END
