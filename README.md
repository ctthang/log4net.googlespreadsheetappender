log4net.googlespreadsheetappender
=================================

A google spreadsheet appender for log4net

What it is?

Google spreadsheet appender is a custom appender for log4net. Its main responsible is to create google spreadsheet file automatically (if not exists) and submit logging events to the google spreadsheet.

Supported Frameworks

1.	.Net compact framework (2.0 or later)

2.	.Net framework (3.5 or later)


Configuration

For .Net Framework
``` xml
<log4net>
    <appender name="TestAppender" type="Log4Net.Full.GoogleSpreadSheetAppender.GoogleSheetAppender, Log4Net.Full.GoogleSpreadSheetAppender">
      <username value="youraccount@gmail.com" />
      <password value="yourpassword" />
      <sheetName value="yourspreadsheetname" />
      <lossy value="true"/>
      <evaluator type="Log4Net.Full.GoogleSpreadSheetAppender.InternetConnectionEvaluator, Log4Net.Full.GoogleSpreadSheetAppender"></evaluator>
    </appender>
    <root>
      <level value="DEBUG" />
      <appender-ref ref="TestAppender" />
    </root>
    <logger name="AppTest">
      <level value="DEBUG" />
      <appender-ref ref="TestAppender" />
    </logger>
  </log4net>
```

For .Net Compact Framework
```xml
<log4net>
    <appender name="TestAppender" type="Log4Net.CP.GoogleSpreadSheetAppender.GoogleSheetAppender, Log4Net.CP.GoogleSpreadSheetAppender">
      <username value="youraccount@gmail.com" />
      <password value="yourpassword" />
      <sheetName value="yoursheetname" />
      <lossy value="true"/>
      <evaluator type="Log4Net.CP.GoogleSpreadSheetAppender.InternetConnectionEvaluator, Log4Net.CP.GoogleSpreadSheetAppender"></evaluator>
    </appender>
    <root>
      <level value="DEBUG" />
      <appender-ref ref="TestAppender" />
    </root>

    <logger name="AppTest">
      <level value="DEBUG" />
      <appender-ref ref="TestAppender" />
    </logger>
  </log4net>

```
