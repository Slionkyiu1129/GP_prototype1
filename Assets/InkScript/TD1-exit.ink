INCLUDE globals.ink

+ {sawtree != "true"} -> start
+ {sawtree == "true" && (getphotokey != "true" || getfountainkey != "true")} -> nokey
+ {sawtree == "true" && (getphotokey == "true" && getfountainkey == "true")} -> havetwokey

===start===
~sawtree="true"
#speaker:lucid #portrait:lucid
感覺這應該是離開這裡的出口。
#speaker:lucid #portrait:lucid
上面寫……
#speaker:lucid #portrait:lucid
需要兩把鑰匙和一組四位數的密碼。
->END

===nokey===
#speaker:lucid #portrait:lucid
還沒拿到鑰匙，再看看附近吧。
->END

===havetwokey===
#speaker:lucid #portrait:lucid
先把鑰匙插入看看吧，然後密碼……
+[輸入密碼]->enterpassword
+[再想想]->END