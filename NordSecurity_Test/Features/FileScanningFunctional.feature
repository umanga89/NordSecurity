Feature: File Scanning Functional

@mytag
Scenario Outline: File Scanning Functional Tests
	Given I have the file scanning library downloaded
	When I invoke ScanFile method with filePath <filePath> and sourceUrl <sourceUrl>
	Then I should get result as <result>
	Examples: 
	| filePath                                                               | sourceUrl                                                                                       | result       |
	| notavirus.exe                                                          | https://totalylegitwebsite.com/freegames/notavirus.exe                                          | MALWARE      |
	| C:\\Users\\Administrator\\Downloads\\firefox.exe                       | https://ferefox.com/downloadfirefox                                                             | MALWARE      |
	| C:\\Music\\Frankie Goes To Hollywood - Welcome To The Pleasuredome.mp3 | http://www.mp3download.cn/fght-welcome-to-the-pleasuredome.mp3                                  | UNDETERMINED |
	| Mike + The Mechanics - Silent Running.mp3.exe                          | ftp://86.15.61.11                                                                               | MALWARE      |
	| ChromeSetup.exe                                                        | https://www.google.com/chrome/thank-you.html?statcb=1&installdataindex=empty&defaultbrowser=0#" | BENIGN       |
	| /home/user/a18063885e58af9eeb47cabfeaa64b52.jpg                        | https://i.pinimg.com/736x/a1/80/63/a18063885e58af9eeb47cabfeaa64b52.jpg                         | BENIGN       |