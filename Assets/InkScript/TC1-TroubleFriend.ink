INCLUDE globals.ink

+{haveGetFlyer != "true"}->noflyer
+{haveGetFlyer == "true" and TroubleFriendGetFlyer!= "true"}->haveflyer
+{TroubleFriendGetFlyer == "true"}->alreadygetflyer

===noflyer===
#speaker:default #portrait:default 
！
（唔哇、是他啊。）
……你看起來急急忙忙的，是在幹嘛啊？
#speaker:lucid #portrait:lucid
沒什麼，只是在找人。
#speaker:default #portrait:default 
哈哈，不會是又惹麻煩了吧？
#speaker:lucid #portrait:lucid
......
->END

===haveflyer===
#speaker:default #portrait:default 
你今天怎麼看起來比平常還神經兮兮的啊……？
+[給傳單]->giveflyer
+[不給傳單]->dontgiveflyer

===dontgiveflyer===
#speaker:lucid #portrait:lucid
我在調查一件事。
#speaker:default #portrait:default 
唔哇，可別把我捲進什麼麻煩事裡，我今天心情已經很差了，別煩我。
->END

===giveflyer===
~ TroubleFriendGetFlyer ="true"
#speaker:lucid #portrait:lucid
這是芙洛爾的尋人啟事。你有看到他嗎？
#speaker:default #portrait:default 
什麼？為什麼弗洛爾會失蹤，你不是總是和他黏在一起嗎？連你都不知道他在哪誰會知道啊。
#speaker:lucid #portrait:lucid
這不是開玩笑的事。
#speaker:default #portrait:default 
……好啦好啦，有什麼新消息我會告訴你……也許啦。
->END

===alreadygetflyer===
就說我會注意了，你煩不煩啊。
->END
