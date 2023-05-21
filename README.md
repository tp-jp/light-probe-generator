# LightProbeGenerator

LigthProbe を簡単に配置できる便利ツールです。同時に配置禁止領域も指定できます。
VRChatのワールド作成時などにご利用ください。

## 導入方法

VCCをインストール済みの場合、以下の**どちらか一つ**の手順を行うことでインポートできます。

- [https://tp-jp.github.io/vpm-repos/](https://tp-jp.github.io/vpm-repos/) へアクセスし、「Add to VCC」をクリック

- VCCのウィンドウで `Setting - Packages - Add Repository` の順に開き、 `https://tp-jp.github.io/vpm-repos/index.json` を追加

[VPM CLI](https://vcc.docs.vrchat.com/vpm/cli/) を使用してインストールする場合、コマンドラインを開き以下のコマンドを入力してください。

```
vpm add repo https://tp-jp.github.io/vpm-repos/index.json
```

## 使い方

1. Packages/LightProbeGenerator/Runtime/Prefab/LightProbeGenerator.prefab を Hierarchy にドラッグ＆ドロップします。

2. Hierarchy上の LightProbeGenerator を選択し、Inspector を表示します。

3. Inspector上で設定を行います。
   
   - Transform
     
     LightProbeGeneratorのギズモを表示することで、LightProbeを配置する領域が確認できます。 Transformを変更し、配置する領域を指定します。
   
   - ProbeSpacing
     
     LightProbeを配置したい間隔を指定します。
   
   - LightProbeの配置を禁止する領域
     
     Addボタンを押下することで、禁止領域（ProhibitedArea）が生成されます。 LightProbe を配置したくない領域がある場合は適宜指定します。

4. Inspector上で LightProbeGenerator のGenerateボタンを押下することで LightProbe が生成されます。
