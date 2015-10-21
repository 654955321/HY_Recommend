using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VayneHunter_Reborn.External.Translation.Languages
{
    /*
     * Korean Translation by Tetrahedrite
     * https://www.joduska.me/forum/user/74465-tetrahedrite/
     */
    class Korean : IVHRLanguage
    {
        public string GetName()
        {
            return "Korean";
        }

        public Dictionary<string, string> GetTranslations()
        {
            var translations = new Dictionary<string, string>
            {
                {"dz191.vhr.combo.useq", "Q 사용"},
                {"dz191.vhr.combo.usee", "E 사용"},
                {"dz191.vhr.combo.user", "R 사용"},
                {"dz191.vhr.combo.mm.q.mana", "Q 마나"},
                {"dz191.vhr.combo.mm.e.mana", "E 마나"},
                {"dz191.vhr.combo.mm.r.mana", "R 마나"},
                {"dz191.vhr.combo.r.minenemies", "최소 R 상대"},
                {"dz191.vhr.combo.q.2wstacks", "타겟에 W가 2스택 있을 경우에만 Q 사용"},
                {"dz191.vhr.mixed.useq", "Q 사용"},
                {"dz191.vhr.mixed.usee", "E 사용"},
                {"dz191.vhr.mixed.mm.q.mana", "Q 마나"},
                {"dz191.vhr.mixed.mm.e.mana", "E 마나"},
                {"dz191.vhr.mixed.q.2wstacks", "타겟에 W가 2스택 있을 경우에만 Q 사용"},
                {"dz191.vhr.mixed.ethird", "세번째 공격을 위해 E 사용"},
                {"dz191.vhr.laneclear.useq", "Q 사용 (Lane Clear)"},
                {"dz191.vhr.laneclear.mm.q.mana", "Q 마나"},
                {"dz191.vhr.lasthit.useq", "Q 사용 (Last Hit)"},
                {"dz191.vhr.lasthit.mm.q.mana", "Q 마나"},
                {"dz191.vhr.farm.condemnjungle", "정글 몹에 선고를 위해 E 사용"},
                {"dz191.vhr.farm.qjungle", "정글 몹에 Q 사용"},
                {"dz191.vhr.misc.condemn.qlogic", "Q 논리"},
                {"dz191.vhr.mixed.mirinQ", "가능할 경우 벽으로 Q (Mirin 모드)"},
                {"dz191.vhr.misc.tumble.smartq", "가능할 경우 QE 시도"},
                {"dz191.vhr.misc.tumble.noaastealthex", "은신 상태일 때 평타 사용 안 함"},
                {"dz191.vhr.misc.tumble.noqenemies", "상대 사이로 Q 사용 안 함"},
                {"dz191.vhr.misc.tumble.dynamicqsafety", "Q를 동적으로 안전한 거리로 사용"},
                {"dz191.vhr.misc.tumble.qspam", "Q 체크 무시"},
                {"dz191.vhr.misc.tumble.qinrange", "Q 킬 스틸"},
                {"dz191.vhr.misc.tumble.walltumble.warning", "벽 구르기를 누르고 계세요"},
                {"dz191.vhr.misc.tumble.walltumble.warning.2", "가까운 벽 구르기 지점으로 이동해서 구를겁니다"},
                {"dz191.vhr.misc.tumble.walltumble", "구르기로 벽 넘기 (벽 구르기)"},
                {"dz191.vhr.misc.condemn.condemnmethod", "선고 방법"},
                {"dz191.vhr.misc.condemn.pushdistance", "E 푸시 거리"},
                {"dz191.vhr.misc.condemn.accuracy", "정확도 (Revolution만 적용)"},
                {"dz191.vhr.misc.condemn.enextauto", "E 자동으로 다음 공격에 사용"},
                {"dz191.vhr.misc.condemn.onlystuncurrent", "현재 타겟만 스턴"},
                {"dz191.vhr.misc.condemn.autoe", "자동 E"},
                {"dz191.vhr.misc.condemn.eks", "스마트 E 킬 스틸"},
                {"dz191.vhr.misc.condemn.noeaa", "타겟이 평타 X회 안에 죽을 경우 E 사용 안함"},
                {"dz191.vhr.misc.condemn.trinketbush", "선고 시 부시에 장신구 사용"},
                {"dz191.vhr.misc.condemn.lowlifepeel", "체력이 적을 때 E로 밀어내기"},
                {"dz191.vhr.misc.condemn.condemnflag", "자르반 4세 깃창에 선고"},
                {"dz191.vhr.misc.condemn.noeturret", "상대 포탑에 E 사용 안 함"},
                {"dz191.vhr.misc.general.antigp", "안티 갭클로저(접근 방지)"},
                {"dz191.vhr.misc.general.interrupt", "인터럽터"},
                {"dz191.vhr.misc.general.antigpdelay", "안티 갭클로저 딜레이 (ms)"},
                {"dz191.vhr.misc.general.specialfocus", "W 2스택 타겟을 포커싱"},
                {"dz191.vhr.misc.general.reveal", "은신 감지 (투명 감지 와드 / 렌즈)"},
                {"dz191.vhr.misc.general.disablemovement", "오브워커 움직임 비활성화"},
                {"dz191.vhr.misc.general.disableattk", "오브워커 평타 비활성화"},
                {"dz191.vhr.draw.spots", "벽 구르기 위치 그리기"},
                {"dz191.vhr.draw.range", "상대 공격 범위 그리기"},
                {"dz191.vhr.draw.qpos", "Reborn Q 위치 (디버그)"},
                {"dz191.vhr.activator.onkey", "액티베이터 키"},
                {"dz191.vhr.activator.always", "항상 켜기"},
                {"dz191.vhr.activator.spells.barrier.onhealth", "체력 % 이하 일 때"},
                {"dz191.vhr.activator.spells.barrier.ls", "Evade/데미지 프레딕션 통합"},
                {"dz191.vhr.activator.spells.heal.onhealth", "체력 % 이하 일 때"},
                {"dz191.vhr.activator.spells.heal.ls", "Evade/데미지 프레딕션 통합"},
                {"dz191.vhr.cleanser.bufftype.Blind", "실명"},
                {"dz191.vhr.cleanser.bufftype.Charm", "매혹"},
                //{"dz191.vhr.cleanser.bufftype.CombatDehancer", ""},
                {"dz191.vhr.cleanser.bufftype.Fear", "공포 (볼리베어)"},
                {"dz191.vhr.cleanser.bufftype.Flee", "공포 (볼리베어 제외)"},
                {"dz191.vhr.cleanser.bufftype.Knockback", "밀치기"},
                {"dz191.vhr.cleanser.bufftype.Knockup", "에어본"},
                {"dz191.vhr.cleanser.bufftype.Polymorph", "변이"},
                {"dz191.vhr.cleanser.bufftype.Silence", "침묵"},
                {"dz191.vhr.cleanser.bufftype.Sleep", "졸음(?)"},
                {"dz191.vhr.cleanser.bufftype.Snare", "속박"},
                {"dz191.vhr.cleanser.bufftype.Stun", "기절"},
                {"dz191.vhr.cleanser.bufftype.Suppression", "제압"},
                {"dz191.vhr.cleanser.bufftype.Taunt", "도발"},
                {"dz191.vhr.cleanser.bufftype.minbuffs", "최소 버프 수"},
                {"dz191.vhr.cleanser.use.combo", "클린저 키"},
                {"dz191.vhr.cleanser.use", "항상 켜기"},
                {"dz191.vhr.ps.profile", "프로필"},
                {"dz191.vhr.ps.load", "불러오기"},
                {"dz191.vhr.ps.savenew", "새 프로필로 저장"},
                {"dz191.vhr.ps.savecurrent", "현재 프로필로 저장"},
                {"dz191.vhr.ps.showdesc", "설명 보기"},
                {"dz191.vhr.ps.delete", "현재 프로필 삭제"},
                {"dz191.vhr.ps.desc1", "채팅에 .rename <새 이름>을 입력하시면 현재 프로필 이름이 변경됩니다."},
                {"dz191.vhr.translations.language", "언어"},
                {"dz191.vhr.translations.select", "번역하기!"},
            };

            return translations;
        }
    }
}

