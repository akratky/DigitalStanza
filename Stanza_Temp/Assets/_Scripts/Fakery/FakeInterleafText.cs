using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FakeInterleafText : MonoBehaviour
{
    private Tuple<string, string> fakeDecachordumPage = new Tuple<string, string>("Decachordum (Vat.lat.1125)",
        "Published in 1507 by Marco Vigerio della Rovere, a cousin of the pope and a Franciscan, whom<br>Julius II elevated to the station of cardinal. This didactic text extends the metaphor of a <color=\"blue\"><link=\"lyre\">lyre</link></color>, " +
        "the instrument of the Psalms, to the structure and organization of its arguments; its topics<br>range from the life, Passion, and Mysteries of <color=\"blue\"><link=\"Christ\">Christ</link></color> and the Holy Family, to the nature of <color=\"blue\"><link=\"angels\">angels</link></color><br>and other theological themes. The ten chords that characterize Vigerios thesis are conceived as<br>the strings of Christian harmony. Vigerio is sometimes thought to have served as one of the<br>Stanzas key intellectual advisors " +
        "(Hartt suggested that <color=\"blue\"><link=\"Bonaventure\">Bonaventure</link></color> in the <color=\"blue\"><link=\"Disputa\">Disputa</link></color> is modeled<br>after his likeness), but the influence of his text is scarcely considered in the context of Raphaels<br>frescoes. Indeed, the manuscript, which is mostly unstudied, is often described as a work of art in<br>its own right. It is splendidly illuminated, with rich miniatures opening each of its ten books.");


    private Tuple<string, string> folOne = new Tuple<string, string>("Fol. Ir",
        "The text of the First Book begins by citing Socrates, <color=\"blue\"><link=\"Plato\">Plato</link></color>, and <color=\"blue\"><link=\"Aristotle\">Aristotle</link></color>. " +
        "As he introduces the<br>mysteries of the faith (particularly relating to the Annunciation), Vigerio explains that even the<br>ancient philosophers, who grasped what is possible to know, could not have comprehended " +
        "these<br>sublime and ineffable examples. Vigerio continues to describe how all ranks of creation,<br>including every immutable variety of being, are connected and revolve mutually in turn,<br>attributing the number, " +
        "measure, and form of the universe to the <color=\"blue\"><link=\"God\">God</link></color> as the architect of the world.<br><br>This epistemological landscape echoes the relationship of " +
        "<color=\"blue\"><link=\"Philosophy\">Philosophy</link></color> and <color=\"blue\"><link=\"Theology\">Theology</link></color> in the<br>Stanza. Knowledge, whose history begins in Philosophy, " +
        "culminates in the Mystery of the<br>Transubstantiation illustrated in Theology, similar to their relationship as described in the<br>opening pages of Vigerios text. Notably, against this backdrop, " +
        "god appears in the painting against the golden firmament of heaven wearing the geometers cap and holding the orb of the world. Also significant is the strong architectural metaphor that characterizes the fresco: " +
        "the<br>marble block at the right of the fresco, as well as the spiritual landscape of Raphaels fictive<br>apse, which is formed from the bodies of the gathered theologians.<br><br>Similar Neoplatonic themes are seen on fol. VIIIr, " +
        "where Vigerio compares the divine to a<br>series of spheres, the highest of which consists of pure intellect, whose center is everywhere and<br>circumference nowhere, whose light illuminates all things. Likewise, the spiritual geography " +
        "of<br>Raphaels composition is defined by a hierarchy of spheres along its vertical axis, embodying a<br>similar cosmic order: the uppermost sphere, the golden vault of heaven, is occupied only by the<br>figure of God, who holds a celestial " +
        "globe and wears a geometers cap. Below him, the mandorla<br>of the sun encircles the figures of Christ, as a circular burst encompasses the dove of the Holy<br>Spirit, mediating between natural and divine. And, finally, on the altar in our " +
        "earthly plane, the<br>wafer of the Eucharist is girded by the glittering gold of the circular monstrance. (See pp. 80-<br>81, Raphaels Art of Commentary and the Library of Julius II on this example).");

    private Tuple<string, string> folTwo = new Tuple<string, string>("Fol. IIr - IIIv",
        "><b>On the ranks of angels (</b><b> (<color=\"blue\"><link=\"angels_image\">detail</link></color>, <color=\"blue\"><link=\"angels_manuscript\">manuscript</link></color>))</b><br><br>This important section in the First Book " +
        "follows the basic structure described by Pseudo-<br>Dionysius, but attending in particular to the distinction of ranks and titles (although Vigerio is<br>not so specific in naming, for example, their individual classes). " +
        "This general celestial hierarchy,<br>which describes ascending ranks of intellect and desire, is organized according to each ranks<br>physical and finite form, suggesting perfection in immateriality. These distinctions run parallel " +
        "to<br>the sensitive materialities that characterize the angels in the Disputa: at the highest level, for<br>example, (<color=\"blue\"><link=\"seraphic-bodies-dissolving-into-golden-planes-of-light\">detail</link></color>)into golden planes of light, " +
        "whereas on the cloudbank below<br>(where the Judeo-Christian Fathers are seated), the <color=\"blue\"><link=\"putti\">putti</link></color> emerge from the misty vapor as<br>fleshy forms. (On the cloud putto in general, see Kleinbub)");

    private Tuple<string, string> folThree = new Tuple<string, string>("Folio CXXr-CXXIr",
        "On the power of the body of Christ in the sacrament of the altar (De p[ote]ntia corporis<br>Chr[ist]I in sacram[en]to altaris)</b><br><br>At the start of the chapter, " +
        "Vigerio compares the body of Christ to the light of a single star that<br>shines in many places  a characterization that is underscored throughout the passage. Stressing<br>the circumstance of the bread, " +
        "as it becomes the flesh and blood of Christ, Vigerio describes the<br>Mystery of the Transubstantiation using metaphors of light. Christ and his sacrifice are compared<br>a lantern, which illuminates the whole of the earth throughout the universe. " +
        "The relationship of<br>the Eucharist to light echoes Raphaels composition, whose theme is the Sacrament of the <color=\"blue\"><link=\"Altar\">Altar</link></color>.<br>Here God appears against the <color=\"blue\"><link=\"firmament\">firmament</link></color> of "+
            "heaven and Christ is encircled by the<br>mandorla of the <color=\"blue\"><link=\"sun\">sun</link></color>. Further, the Disputa is the only fresco in the Stanza to use the "+
            "technique of<br>pastiglia, or wax leafed with <color=\"blue\"><link=\"gold\">gold</link></color>, "+
            "evoking the qualities of light and illumination that<br>characterize Vigerios text. Finally, the light in Raphaels fresco illuminates the composition,<br>shining down on an assembled world of priests, pontiffs, and theologians.");

    private Tuple<string, string> furtherReading = new Tuple<string, string>("Further Reading",
        "Tracy Cosgriff, The Library of Julius II and Raphaels Art of Commentary,<b> " +
        "I Tatti Studies in<br>the Italian Renaissance</b> 22.1 (2019): 59-91.<br>Frederick Hartt, Lignum Vitae in Medio Paradisi: The Stanza dEliodoro and the Sistine<br>Ceiling, <b>Art Bulletin</b> 32.2 (1950): 115-145.<br>Christian Kleinbub, " +
        "At the Boundaries of Sight: The Italian Renaissance Cloud Putto. In<br><b>Renaissance Theories of Vision</b>, ed. John Hendrix and Charles Carman, 117-133. London and<br>New York: Routledge, 2010.");
    
    private Tuple<string, string>[] fakePages = new Tuple<string, string>[5];
    private int _pageIndex = 0;
    public TMP_Text headerText;
    private TMP_Text textMeshText;
    
// Start is called before the first frame update
    void Start()
    {
        fakePages[0] = fakeDecachordumPage;
        fakePages[1] = folOne;
        fakePages[2] = folTwo;
        fakePages[3] = folThree;
        fakePages[4] = furtherReading;
        
        
        textMeshText = GetComponent<TMP_Text>();
        headerText.text = fakePages[_pageIndex].Item1;
        textMeshText.text = fakePages[_pageIndex].Item2;
    }

    public void NextInterleafPage()
    {
        if (textMeshText.pageToDisplay != textMeshText.textInfo.pageCount)
        {
            textMeshText.pageToDisplay++;
        }
        else if (_pageIndex + 1 != fakePages.Length)
        {
            _pageIndex++;
            headerText.text = fakePages[_pageIndex].Item1;
            textMeshText.text = fakePages[_pageIndex].Item2;
            textMeshText.pageToDisplay = 1;
        }
    }

    public void PrevInterleafPage()
    {
        if (textMeshText.pageToDisplay - 1  > 0)
        {
            textMeshText.pageToDisplay--;
        }
        else if (_pageIndex - 1 >= 0)
        {
            _pageIndex--;
            headerText.text = fakePages[_pageIndex].Item1;
            textMeshText.text = fakePages[_pageIndex].Item2;
            textMeshText.pageToDisplay = 1;
        }
    }


}
