package com.test.hotdll;

import android.app.AlarmManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.ContextWrapper;
import android.content.Intent;

import com.unity3d.player.UnityPlayer;

import static android.app.PendingIntent.FLAG_ONE_SHOT;

public class TestDll {
    //获得包名
    public static String GetPackageName() {
        return UnityPlayer.currentActivity.getPackageName();
    }
    //重启游戏
    public static void Reboot() {
        ContextWrapper ctx = UnityPlayer.currentActivity;
        Intent intent = ctx.getBaseContext().getPackageManager().getLaunchIntentForPackage(ctx.getBaseContext().getPackageName());
        PendingIntent restartIntent = PendingIntent.getActivity(ctx.getApplicationContext(), 0, intent, PendingIntent.FLAG_ONE_SHOT);
        AlarmManager mgr = (AlarmManager) ctx.getSystemService(Context.ALARM_SERVICE);
        mgr.set(AlarmManager.RTC, System.currentTimeMillis() + 1000, restartIntent);
        System.exit(0);
    }
}
