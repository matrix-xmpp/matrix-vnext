/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
 * See the COPYING file for more information.
 *
 * This file is part of the MatriX project.
 *
 * NOTICE: All information contained herein is, and remains the property
 * of AG-Software and its suppliers, if any.
 * The intellectual and technical concepts contained herein are proprietary
 * to AG-Software and its suppliers and may be covered by German and Foreign Patents,
 * patents in process, and are protected by trade secret or copyright law.
 *
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from AG-Software.
 *
 * Contact information for AG-Software is available at http://www.ag-software.de
 */

using Matrix.Attributes;

namespace Matrix.Xmpp.Mood
{
    public enum Moods
    {
        /// <summary>
        /// No mood set
        /// </summary>
        None,

        /// <summary>
        /// Impressed with fear or apprehension; in fear; apprehensive.
        /// </summary>
        [Name("afraid")]
        Afraid,

        /// <summary>
        ///  Astonished; confounded with fear, surprise or wonder.
        /// </summary>
        [Name("amazed")]
        Amazed,

        /// <summary>
        /// Inclined to love; having a propensity to love, or to sexual enjoyment; loving, fond, affectionate, passionate, lustful, sexual, etc.
        /// </summary>
        [Name("amorous")]
        Amorous,

        /// <summary>
        /// Displaying or feeling anger, i.e., a strong feeling of displeasure, hostility or antagonism towards someone or something, usually combined with an urge to harm.
        /// </summary>
        [Name("angry")]
        Angry,

        /// <summary>
        /// To be disturbed or irritated, especially by continued or repeated acts.
        /// </summary>
        [Name("annoyed")]
        Annoyed,

        /// <summary>
        /// Full of anxiety or disquietude; greatly concerned or solicitous, esp. respecting something future or unknown; being in painful suspense.
        /// </summary>
        [Name("anxious")]
        Anxious,

        /// <summary>
        /// To be stimulated in one's feelings, especially to be sexually stimulated.
        /// </summary>
        [Name("aroused")]
        Aroused,

        /// <summary>
        ///Feeling shame or guilt.
        /// </summary>
        [Name("ashamed")]
        Ashamed, 

        /// <summary>
        /// Suffering from boredom; uninterested, without attention.
        /// </summary>
        [Name("bored")]
        Bored,

        /// <summary>
        /// Strong in the face of fear; courageous.
        /// </summary>
        [Name("brave")]
        Brave,

        /// <summary>
        /// Peaceful, quiet.
        /// </summary>
        [Name("calm")]
        Calm,

        /// <summary>
        /// Taking care or caution; tentative.
        /// </summary>
        [Name("cautious")]
        Cautious,

        /// <summary>
        ///Feeling the sensation of coldness, especially to the point of discomfort.
        /// </summary>
        [Name("cold")]
        Cold,

        /// <summary>
        /// Feeling very sure of or positive about something, especially about one's own capabilities.
        /// </summary>
        [Name("confident")]
        Confident,

        /// <summary>
        /// Chaotic, jumbled or muddled.
        /// </summary>
        [Name("confused")]
        Confused,

        /// <summary>
        /// Feeling introspective or thoughtful.
        /// </summary>
        [Name("contemplative")]
        Contemplative,

        /// <summary>
        /// Pleased at the satisfaction of a want or desire; satisfied.
        /// </summary>
        [Name("contented")]
        Contented,

        /// <summary>
        /// Grouchy, irritable; easily upset.
        /// </summary>
        [Name("cranky")]
        Cranky,

        /// <summary>
        /// Feeling out of control; feeling overly excited or enthusiastic.
        /// </summary>
        [Name("crazy")]
        Crazy,

        /// <summary>
        /// Feeling original, expressive, or imaginative.
        /// </summary>
        [Name("creative")]
        Creative,

        /// <summary>
        /// Inquisitive; tending to ask questions, investigate, or explore.
        /// </summary>
        [Name("curious")]
        Curious,

        /// <summary>
        /// Feeling sad and dispirited.
        /// </summary>
        [Name("dejected")]
        Dejected,

        /// <summary>
        /// Severely despondent and unhappy.
        /// </summary>
        [Name("depressed")]
        Depressed,

        /// <summary>
        /// Defeated of expectation or hope; let down.
        /// </summary>
        [Name("disappointed")]
        Disappointed,

        /// <summary>
        /// Filled with disgust; irritated and out of patience.
        /// </summary>
        [Name("disgusted")]
        Disgusted,

        /// <summary>
        /// Feeling a sudden or complete loss of courage in the face of trouble or danger.
        /// </summary>
        [Name("dismayed")]
        Dismayed,
        
        /// <summary>
        /// Having one's attention diverted; preoccupied.
        /// </summary>
        [Name("distracted")]
        Distracted,

        /// <summary>
        /// Having a feeling of shameful discomfort.
        /// </summary>
        [Name("embarrassed")]
        Embarrassed,

        /// <summary>
        /// Feeling pain by the excellence or good fortune of another.
        /// </summary>
        [Name("envious")]
        Envious,

        /// <summary>
        /// Having great enthusiasm.
        /// </summary>
        [Name("excited")]
        Excited,

        /// <summary>
        /// In the mood for flirting.
        /// </summary>
        [Name("flirtatious")]
        Flirtatious,

        /// <summary>
        /// Suffering from frustration; dissatisfied, agitated, or discontented because one is unable to perform an action or fulfill a desire.
        /// </summary>
        [Name("frustrated")]
        Frustrated,

        /// <summary>
        /// Feeling appreciation or thanks.
        /// </summary>
        [Name("grateful")]
        Grateful,

         /// <summary>
         ///Feeling very sad about something, especially something lost; mournful; sorrowful.
         /// </summary>
        [Name("grieving")]
        Grieving,

        /// <summary>
        /// Unhappy and irritable.
        /// </summary>
        [Name("grumpy")]
        Grumpy,

         /// <summary>
         ///Feeling responsible for wrongdoing; feeling blameworthy.
         /// </summary>
        [Name("guilty")]
        Guilty,

        /// <summary>
        /// Experiencing the effect of favourable fortune; having the feeling arising from the consciousness of well-being or of enjoyment; enjoying good of any kind, as peace, tranquillity, comfort; contented; joyous.
        /// </summary>
        [Name("happy")]
        Happy,

        /// <summary>
        /// Having a positive feeling, belief, or expectation that something wished for can or will happen.
        /// </summary>
        [Name("hopeful")]
        Hopeful,

        /// <summary>
        /// Feeling the sensation of heat, especially to the point of discomfort.
        /// </summary>
        [Name("hot")]
        Hot,

        /// <summary>
        /// Having or showing a modest or low estimate of one's own importance; feeling lowered in dignity or importance.
        /// </summary>
        [Name("humbled")]
        Humbled,

        /// <summary>
        /// Feeling deprived of dignity or self-respect.
        /// </summary>
        [Name("humiliated")]
        Humiliated,

        /// <summary>
        /// Having a physical need for food.
        /// </summary>
        [Name("hungry")]
        Hungry,

        /// <summary>
        /// Wounded, injured, or pained, whether physically or emotionally.
        /// </summary>
        [Name("hurt")]
        Hurt,

        /// <summary>
        /// Favourably affected by something or someone.
        /// </summary>
        [Name("impressed")]
        Impressed,

        /// <summary>
        /// Feeling amazement at something or someone; or feeling a combination of fear and reverence.
        /// </summary>
        [Name("in_awe")]
        InAwe,

        /// <summary>
        /// Feeling strong affection, care, liking, or attraction..
        /// </summary>
        [Name("in_love")]
        InLove,

        /// <summary>
        /// The indignant
        /// </summary>
        [Name("indignant")]
        Indignant,

        /// <summary>
        /// Showing great attention to something or someone; having or showing interest.
        /// </summary>
        [Name("interested")]
        Interested,

        /// <summary>
        /// Under the influence of alcohol; drunk.
        /// </summary>
        [Name("intoxicated")]
        Intoxicated,

        /// <summary>
        /// Feeling as if one cannot be defeated, overcome or denied.
        /// </summary>
        [Name("invincible")]
        Invincible,

        /// <summary>
        /// Fearful of being replaced in position or affection.
        /// </summary>
        [Name("jealous")]
        Jealous,

        /// <summary>
        /// Feeling isolated, empty, or abandoned.
        /// </summary>
        [Name("lonely")]
        Lonely,

        /// <summary>
        /// Unable to find one's way, either physically or emotionally.
        /// </summary>
        [Name("lost")]
        Lost,

        /// <summary>
        /// Feeling as if one will be favored by luck.
        /// </summary>
        [Name("lucky")]
        Lucky,

        /// <summary>
        /// Causing or intending to cause intentional harm; bearing ill will towards another; cruel; malicious.
        /// </summary>
        [Name("mean")]
        Mean,

        /// <summary>
        /// Given to sudden or frequent changes of mind or feeling; temperamental.
        /// </summary>
        [Name("moody")]
        Moody,

        /// <summary>
        /// Easily agitated or alarmed; apprehensive or anxious.
        /// </summary>
        [Name("nervous")]
        Nervous,

        /// <summary>
        /// Not having a strong mood or emotional state.
        /// </summary>
        [Name("neutral")]
        Neutral,

        /// <summary>
        /// Feeling emotionally hurt, displeased, or insulted.
        /// </summary>
        [Name("offended")]
        Offended,

        /// <summary>
        /// Feeling resentful anger caused by an extremely violent or vicious attack, or by an offensive, immoral, or indecent act.
        /// </summary>
        [Name("outraged")]
        Outraged,

        /// <summary>
        /// Interested in play; fun, recreational, unserious, lighthearted; joking, silly.
        /// </summary>
        [Name("playful")]
        Playful,

        /// <summary>
        ///Feeling a sense of one's own worth or accomplishment.
        /// </summary>
        [Name("proud")]
        Proud,

        /// <summary>
        /// Having an easy-going mood; not stressed; calm.
        /// </summary>
        [Name("relaxed")]
        Relaxed,

        /// <summary>
        /// Feeling uplifted because of the removal of stress or discomfort.
        /// </summary>
        [Name("relieved")]
        Relieved,

        /// <summary>
        /// Feeling regret or sadness for doing something wrong.
        /// </summary>
        [Name("remorseful")]
        Remorseful,

        /// <summary>
        /// Without rest; unable to be still or quiet; uneasy; continually moving.
        /// </summary>
        [Name("restless")]
        Restless,

        /// <summary>
        /// Feeling sorrow; sorrowful, mournful.
        /// </summary>
        [Name("sad")]
        Sad,

        /// <summary>
        /// Mocking and ironical.
        /// </summary>
        [Name("sarcastic")]
        Sarcastic,

        /// <summary>
        /// Pleased at the fulfillment of a need or desire.
        /// </summary>
        [Name("satisfied")]
        Satisfied,

        /// <summary>
        /// Without humor or expression of happiness; grave in manner or disposition; earnest; thoughtful; solemn.
        /// </summary>
        [Name("serious")]
        Serious,

        /// <summary>
        /// Surprised, startled, confused, or taken aback.
        /// </summary>
        [Name("shocked")]
        Shocked,

        /// <summary>
        /// Feeling easily frightened or scared; timid; reserved or coy.
        /// </summary>
        [Name("shy")]
        Shy,

        /// <summary>
        /// Feeling in poor health; ill.
        /// </summary>
        [Name("sick")]
        Sick,

        /// <summary>
        /// Feeling the need for sleep.
        /// </summary>
        [Name("sleepy")]
        Sleepy,

        /// <summary>
        /// Acting without planning; natural; impulsive.
        /// </summary>
        [Name("spontaneous")]
        Spontaneous,

        /// <summary>
        /// Suffering emotional pressure.
        /// </summary>
        [Name("stressed")]
        Stressed,

        /// <summary>
        /// Capable of producing great physical force; or, emotionally forceful, able, determined, unyielding.
        /// </summary>
        [Name("strong")]
        Strong,

        /// <summary>
        /// Experiencing a feeling caused by something unexpected.
        /// </summary>
        [Name("surprised")]
        Surprised,

        /// <summary>
        /// Showing appreciation or gratitude.
        /// </summary>
        [Name("thankful")]
        Thankful,

        /// <summary>
        /// Feeling the need to drink.
        /// </summary>
        [Name("thirsty")]
        Thirsty,

        /// <summary>
        /// In need of rest or sleep.
        /// </summary>
        [Name("tired")]
        Tired,

        /// <summary>
        /// [Feeling any emotion not defined here.]
        /// </summary>
        [Name("undefined")]
        Undefined,

        /// <summary>
        /// Lacking in force or ability, either physical or emotional.
        /// </summary>
        [Name("weak")]
        Weak,

        /// <summary>
        /// Thinking about unpleasant things that have happened or that might happen; feeling afraid and unhappy.
        /// </summary>
        [Name("worried")]
        Worried,
    }
}
