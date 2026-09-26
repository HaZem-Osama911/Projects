<?php

namespace App\Http\Controllers;

use Illuminate\Contracts\View\View;

class PronunciationController extends Controller
{
    public function __invoke(): View
    {
        return view('pronunciation', [
            'arabicUrl' => config('services.pronunciation.arabic_url'),
            'englishUrl' => config('services.pronunciation.english_url'),
        ]);
    }
}
