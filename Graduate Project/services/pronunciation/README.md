# Pronunciation services

خدمتان مستقلتان بـ Flask لتمارين النطق العربي والإنجليزي. كل خدمة تعرض
واجهتها وAPI الخاص بها من نفس الـ origin، وتستخدم Whisper مع FFmpeg محمول
داخل بيئة Python.

## التثبيت

من داخل `services\pronunciation`:

```cmd
python -m venv .venv
.venv\Scripts\python.exe -m pip install -r requirements.txt
```

## التشغيل

العربي:

```cmd
cd /d arabic
..\.venv\Scripts\python.exe server.py
```

الإنجليزي، من نافذة أخرى:

```cmd
cd /d english
..\.venv\Scripts\python.exe server.py
```

الخدمة العربية تعمل على `127.0.0.1:5000` والإنجليزية على
`127.0.0.1:5001`. يمكن تغيير العنوان والمنفذ باستخدام
`PRONUNCIATION_HOST` و`PRONUNCIATION_PORT`.

## الاختبارات

من داخل `services\pronunciation`:

```cmd
.venv\Scripts\python.exe -m unittest discover -s tests -v
```

لاختبار نماذج Whisper الحقيقية بعد تنزيل `small` و`base`:

```cmd
set RUN_WHISPER_INTEGRATION=1
.venv\Scripts\python.exe -m unittest discover -s tests -p test_whisper_integration.py -v
```

الملفات الصوتية محدودة بـ10 MB، وتحصل على اسم مؤقت مولد من الخادم، وتُحذف
بعد المعالجة في جميع الحالات.
