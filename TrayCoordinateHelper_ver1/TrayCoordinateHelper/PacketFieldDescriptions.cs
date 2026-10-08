namespace TrayCoordinateHelper
{
    // 사용자가 제공한 구조체 순서에만 적용하는 설명표입니다.
    // 다른 프로토콜/구조체는 이 표를 수정하거나 별도 프로필로 분리해야 합니다.
    public static class PacketFieldDescriptions
    {
        private static readonly string[] Names = {
            "Prefix", "데이터 길이", "커맨드 헤더", "캠 ID", "검사 종류",
            "트리거 타입", "캡처 Done", "스트립 바코드", "스트립 시퀀스",
            "익스팬더 테이블 번호", "X (의미 확인 필요)", "Z축 번호",
            "Has Device", "X 위치", "Y 위치"
        };

        public static string Describe(int index, uint value)
        {
            if (index < 0 || index >= Names.Length) return "미정의 항목 #" + index;
            if (index == 3)
                return value == 2 ? "캠 ID: 02 · 사이드 검사" : "캠 ID: " + value + " · 미등록";
            if (index == 4)
                return value == 13 ? "검사 종류: 13 · 사이드 비전검사" : "검사 종류: " + value + " · 미등록";
            return Names[index];
        }
    }
}
