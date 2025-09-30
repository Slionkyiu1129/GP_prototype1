INCLUDE globals.ink

+{talktofluor!="true"}->beforetalk
+{talktofluor=="true"&&getfountainkey!="true"}->talk
+{talktofluor=="true"&&getfountainkey=="true"}->alreadygetkey

===beforetalk===
#speaker:lucid #portrait:lucid
一個噴泉。
->END

===talk===
~ getfountainkey = "true"
#speaker:lucid #portrait:lucid
Fluor說這裡有禮物。
#speaker:lucid #portrait:lucid
讓我找找。
#speaker:lucid #portrait:lucid
一把鑰匙，先收起來好了，或許用的到。
-> END

===alreadygetkey===
#speaker:lucid #portrait:lucid
這裡已經沒有東西了。
->END